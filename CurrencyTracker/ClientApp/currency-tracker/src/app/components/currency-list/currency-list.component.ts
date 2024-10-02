import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { CurrencyService } from '../../services/currency.service';
import { CurrencyRateInfoDto } from '../../models/currency-rate-info.model';
import { SubscriptionDialogComponent } from '../subscription-dialog/subscription-dialog.component';

@Component({
  selector: 'app-currency-list',
  templateUrl: './currency-list.component.html',
  styleUrls: ['./currency-list.component.scss']
})
export class CurrencyListComponent implements OnInit {
  currencies: CurrencyRateInfoDto[] = [];

  constructor(
    private currencyService: CurrencyService,
    private router: Router,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.getCurrencyList();
  }

  getCurrencyList(): void {
    this.currencyService.getAllCurrencyRateInfos().subscribe(
      (data: CurrencyRateInfoDto[]) => {
        this.currencies = data;
      },
      error => {
        console.error('Error:', error);
      }
    );
  }

  viewCurrencyHistory(code: string): void {
    this.router.navigate(['/currency-history', code]);
  }

  openSubscriptionDialog(): void {
    const dialogRef = this.dialog.open(SubscriptionDialogComponent, {
      width: '400px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.subscribeToCurrency(result);
      }
    });
  }

  subscribeToCurrency(code: string): void {
    this.currencyService.subscribeToCurrency(code).subscribe(
      () => {
        alert(`Successfully subscribed to the currency: ${code}`);
      },
      error => {
        console.error('Error:', error);
        alert('Error subscribing to the currency');
      }
    );
  }
}
