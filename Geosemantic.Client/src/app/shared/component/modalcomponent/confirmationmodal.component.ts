import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material";

@Component({
  templateUrl: 'confirmationmodal.component.html',
  encapsulation: ViewEncapsulation.None,
})

export class ConfirmationModalComponent {

  body: string;
  title: string;

  ngOnInit(): void {
    
  }

  constructor(private dialogRef: MatDialogRef<ConfirmationModalComponent>, @Inject(MAT_DIALOG_DATA) data) {
    this.body = data.body;
    this.title = data.title;
  }

  onConfirm(): void {
    this.dialogRef.close(true);
  }

  onCancel(): void {
    this.dialogRef.close(false);
  }
}
