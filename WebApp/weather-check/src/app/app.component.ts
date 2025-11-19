import { CommonModule } from '@angular/common';
import { Component, computed, inject, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { City, Region, WeatherData, WeatherView } from './models/weather.models';
import { WeatherService } from './services/weather.service';
import { lastValueFrom } from 'rxjs';
import { toSignal } from '@angular/core/rxjs-interop'; // Import toSignal utility


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,     CommonModule,
    MatCardModule, MatFormFieldModule, MatSelectModule, MatInputModule, MatProgressSpinnerModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'weather-check';
  weatherService = inject(WeatherService);

    selectedRegionId = signal<number | null>(null);
  selectedCityId = signal<number | null>(null);
  selectedView = signal<WeatherView['id']>('current'); // Default view
  
  // Data arrays
  regions = toSignal<Region[]>(this.weatherService.getAllRegions());
  cities = signal<City[]>([]);
allWeatherDataByCity = signal<WeatherData[]>([]);
  
  // Loading states (simulate async loading for better UX)
  citiesLoading = signal(false);
  weatherLoading = signal(false);

  // --- VIEW DATA (Fixed/Static) ---
  views: WeatherView[] = [
    { id: 'current', name: 'Current data' },
    { id: 'all', name: 'All data' }
  ];


    // 1. Cities filtered by selected region (triggered by selectedRegionId)
  displayedWeatherData = computed(() => {
    const allData = this.allWeatherDataByCity();
    const view = this.selectedView();

    if (view === 'current' && allData.length > 0) {
      // Only show the newest entry (first in the sorted list)
      return [allData[0]];
    }
    return allData; // Show all data
  });
  
  // Helper to get the current selected city object
  selectedCity = computed(() => {
    const cityId = this.selectedCityId();
    if (!cityId) return null;
    return this.cities().find(c => c.id === cityId) || null;
  });

  // --- EVENT HANDLERS (Now handling Observables) ---
  
  onRegionChange(regionId: number): void {
    this.selectedRegionId.set(regionId);
    this.selectedCityId.set(null); 
    this.cities.set([]); // Clear city data immediately
    this.allWeatherDataByCity.set([]); // Clear weather data

    if (!regionId) return;

    // Start loading and subscribe to the Observable
    this.citiesLoading.set(true);
    this.weatherService.getAllCitiesByRegion(regionId).subscribe({
      next: (cities) => {
        this.cities.set(cities);
        this.citiesLoading.set(false);
        
        // Auto-select the first city after load
        if (cities.length > 0) {
            this.selectedCityId.set(cities[0].id);
            // Manually trigger city change logic for the newly auto-selected city
            this.onCityChange(cities[0].id); 
        }
      },
      error: (err) => {
        console.error("Failed to load cities:", err);
        this.citiesLoading.set(false);
      }
    });
  }
  
  onCityChange(cityId: number): void {
    this.selectedCityId.set(cityId);
    this.allWeatherDataByCity.set([]); // Clear old weather data

    if (!cityId) return;

    // Start loading and subscribe to the Observable
    this.weatherLoading.set(true);
    this.weatherService.getAllWeatherData(cityId).subscribe({
      next: (data) => {
        this.allWeatherDataByCity.set(data);
        this.weatherLoading.set(false);
      },
      error: (err) => {
        console.error("Failed to load weather data:", err);
        this.weatherLoading.set(false);
      }
    });
  }
}
