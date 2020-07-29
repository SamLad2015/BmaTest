import { Component, Input, OnInit } from '@angular/core';
import { ViewCell } from 'ng2-smart-table';

@Component({
  selector: 'app-checkbox',
  template: `
    <div class="check-box"><input
      type="checkbox"
      disabled
      (click)="changeBoolean()"
      [checked]="this.checked"></div>

  `
})
export class CheckboxComponent implements ViewCell, OnInit {

  @Input() value: any;
  @Input() rowData: any;

  checked: boolean;

  constructor() { }

  ngOnInit() {
    this.checked = this.value;
  }

  changeBoolean() {
    this.checked = !this.checked;
  }

}
