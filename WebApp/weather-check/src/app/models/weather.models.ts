export interface Region {
    id: number;
    name: string;
    cities?: City[];
}

export interface Region {
    id: number;
    name: string;
    cities?: City[];
}

export interface City {
    id: number;
    name: string;
    regionId: number;
    region: Region;
    weatherData?: WeatherData[];
}

export interface Condition{
    id: number;
    name: string;
    description: string;
    weatherData?: WeatherData[];
}

export interface WeatherData {
    id: number;
    cityId: number;
    city: City;
    date: Date;
    temperatureCelsius: number;
    humidityPercent: number;
    windSpeedKph: number;
    conditionId: number;
    condition: Condition;
}

export interface WeatherView {
  id: 'current' | 'all';
  name: string;
}