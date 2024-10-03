import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CurrencyRateInfoDto } from '../models/currency-rate-info.model';
import { ExchangeRateDto } from '../models/exchange-rate.model';

@Injectable({
  providedIn: 'root'
})
export class CurrencyService {
  private baseUrl = 'http://localhost:5284/api/currency';

  constructor(private http: HttpClient) {}

  getAllCurrencyRateInfos(): Observable<CurrencyRateInfoDto[]> {
    return this.http.get<CurrencyRateInfoDto[]>(`${this.baseUrl}/all-rate-info`);
  }

  getCurrencyHistory(code: string): Observable<ExchangeRateDto[]> {
    return this.http.get<ExchangeRateDto[]>(`${this.baseUrl}/${code}/history`);
  }

  subscribeToCurrency(code: string): Observable<any> {
    return this.http.post(`${this.baseUrl}/${code}/subscribe`, {});
  }

  unsubscribeToCurrency(code: string): Observable<any> {
    return this.http.post(`${this.baseUrl}/${code}/unsubscribe`, {});
  }

  calculateEMA(code: string, date: Date, days: number): Observable<any> {
    const dateString = date.toISOString().split('T')[0];
    return this.http.get(`${this.baseUrl}/${code}/calculate-ema?date=${dateString}&days=${days}`);
  }
}