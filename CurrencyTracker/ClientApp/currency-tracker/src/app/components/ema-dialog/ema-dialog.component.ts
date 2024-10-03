import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CurrencyService } from '../../services/currency.service';

@Component({
  selector: 'app-ema-dialog',
  templateUrl: './ema-dialog.component.html',
  styleUrls: ['./ema-dialog.component.scss']
})
export class EmaDialogComponent {
  data: { code: string, date: Date, days: number };
  result: any = null;
  loading: boolean = false; 
  error: string | null = null; 

  constructor(
    public dialogRef: MatDialogRef<EmaDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public inputData: { code: string,date: Date, days: number },
    private currencyService: CurrencyService
  ) {
    this.data = {
      code: inputData.code,
      date: inputData.date,
      days: inputData.days
    };
  }

  onConfirm(): void {
    this.loading = true;
    this.error = null;

    this.currencyService.calculateEMA(this.data.code, this.data.date, this.data.days).subscribe({
      next: (res) => {
        this.result = res;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Error calculating EMA';
        this.loading = false;
      }
    });
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
