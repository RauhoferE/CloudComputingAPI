import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { City, Region, WeatherData } from '../models/weather.models';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class WeatherService {

  constructor(private http: HttpClient) 
  { 

  }

  public getAllRegions(): Observable<Region[]>{
    return this.http.get<Region[]>(`${environment.apiUrl}/weather/regions`);
  }

  public getAllCitiesByRegion(regionId: number): Observable<City[]>{
    return this.http.get<City[]>(`${environment.apiUrl}/cities?regionId=${regionId}`);
  }

  public getLatestWeatherData(cityId: number): Observable<WeatherData>{
    return this.http.get<WeatherData>(`${environment.apiUrl}/weather/latest?cityId=${cityId}`);
  }

  public getAllWeatherData(cityId: number): Observable<WeatherData[]>{  
    return this.http.get<WeatherData[]>(`${environment.apiUrl}/weather/all?cityId=${cityId}`);
  }
}
