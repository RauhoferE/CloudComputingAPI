export interface IdName{
    id: number;
    name: string;
}

export interface Condition extends IdName{
    description: string;
}

export interface WeatherData {
    id: number;
    cityId: number;
    city: IdName;
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