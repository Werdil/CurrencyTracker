import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CurrencyService } from '../../services/currency.service';
import { ExchangeRateDto } from '../../models/exchange-rate.model';

@Component({
  selector: 'app-currency-history',
  templateUrl: './currency-history.component.html',
  styleUrls: ['./currency-history.component.scss']
})
export class CurrencyHistoryComponent implements OnInit {
  exchangeRates: ExchangeRateDto[] = [];
  currencyCode: string | null = null;

  constructor(private route: ActivatedRoute, private currencyService: CurrencyService) {}

  ngOnInit(): void {
    this.currencyCode = this.route.snapshot.paramMap.get('code');
    if (this.currencyCode) {
      this.getCurrencyHistory(this.currencyCode);
    }
  }

  getCurrencyHistory(code: string): void {
    this.currencyService.getCurrencyHistory(code).subscribe(
      (data: ExchangeRateDto[]) => {
        this.exchangeRates = data;
      },
      error => {
        console.error('Error:', error);
      }
    );
  }
}
