import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IdName, WeatherData } from '../models/weather.models';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class WeatherService {

  constructor(private http: HttpClient) 
  { 

  }

  public getAllRegions(): Observable<IdName[]>{
    return this.http.get<IdName[]>(`${environment.apiUrl}/weather/regions`);
  }

  public getAllCitiesByRegion(regionId: number): Observable<IdName[]>{
    return this.http.get<IdName[]>(`${environment.apiUrl}/weather/cities?regionId=${regionId}`);
  }

  public getLatestWeatherData(cityId: number): Observable<WeatherData>{
    return this.http.get<WeatherData>(`${environment.apiUrl}/weather/latest?cityId=${cityId}`);
  }

  public getAllWeatherData(cityId: number): Observable<WeatherData[]>{  
    return this.http.get<WeatherData[]>(`${environment.apiUrl}/weather/all?cityId=${cityId}`);
  }
}
