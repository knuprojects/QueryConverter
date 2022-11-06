import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import * as events from "events";

@Component({
  selector: 'app-converter',
  templateUrl: './converter.component.html',
  styleUrls: ['./converter.component.scss']
})
export class ConverterComponent implements OnInit {
  public commandsForm!: FormGroup;
  public commands: any[] = [
    { name: 'SELECT' },
    { name: 'ORDER BY' },
    { name: 'GROUP BY' }
  ];

  constructor(private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.commandsForm = this.formBuilder.group({
      command: ['', Validators.required],
      SQLQuery: ['', Validators.required]
    })
  }

  public sendCommand(): void {
    console.log(this.commandsForm.value[0])
  }
}
