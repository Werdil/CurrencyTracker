import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { CurrencyService } from '../../services/currency.service';
import { CurrencyRateInfoDto } from '../../models/currency-rate-info.model';
import { SubscriptionDialogComponent } from '../subscription-dialog/subscription-dialog.component';
import { EmaDialogComponent } from '../ema-dialog/ema-dialog.component';

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

  unsubscribeCurrency(code: string): void {
    this.currencyService.unsubscribeToCurrency(code).subscribe(() => {
      this.getCurrencyList();
    }, error => {
      console.error('Error unsubscribing:', error);
    });
  }

  viewCurrencyHistory(code: string): void {
    this.router.navigate(['/currency-history', code]);
  }

  openSubscriptionDialog(): void {
    const dialogRef = this.dialog.open(SubscriptionDialogComponent, {
      width: '300px'
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
        this.getCurrencyList();
      },
      error => {
        console.error('Error:', error);
      }
    );
  }

  openEmaDialog(code: string): void {
    const dialogRef = this.dialog.open(EmaDialogComponent, {
      width: '300px',
      data: { code:code, date: new Date(), days: 14 } // Domyślne wartości
    });
  }

}
