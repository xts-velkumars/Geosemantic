import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material";

@Component({
  templateUrl: 'genericmessagemodal.component.html',
  encapsulation: ViewEncapsulation.None,
})

export class GenericMessageModalComponent {

  body: string;
  title: string;

  ngOnInit(): void {

  }

  constructor(private dialogRef: MatDialogRef<GenericMessageModalComponent>, @Inject(MAT_DIALOG_DATA) data) {
    this.body = data.body;
    this.title = data.title;
  }

  onOk(): void {
    this.dialogRef.close(true);
  }

}
