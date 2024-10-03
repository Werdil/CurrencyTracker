import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-subscription-dialog',
  templateUrl: './subscription-dialog.component.html',
  styleUrls: ['./subscription-dialog.component.scss']
})
export class SubscriptionDialogComponent {

  currencies: string[] = [
    'AUD', 'BGN', 'BRL', 'CAD', 'CHF', 'CNY', 'CZK', 'DKK', 'EUR',
    'GBP', 'HKD', 'HUF', 'IDR', 'ILS', 'INR', 'ISK', 'JPY',
    'KRW', 'MXN', 'MYR', 'NOK', 'NZD', 'PHP', 'RON', 'RSD', 'RUB',
    'SEK', 'SGD', 'THB', 'TRY', 'UAH', 'USD', 'ZAR'
  ];

  selectedCurrency: string = '';

  constructor(
    public dialogRef: MatDialogRef<SubscriptionDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { name: string }
  ) {}

  onCancel(): void {
    this.dialogRef.close();
  }

  onSubscribe(): void {
    this.dialogRef.close(this.selectedCurrency);
  }
}
