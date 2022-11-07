import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { QueryService } from "../services/query.service";
import {Observable} from "rxjs";
import {ResultModel} from "../models/resultModel";

@Component({
  selector: 'app-converter',
  templateUrl: './converter.component.html',
  styleUrls: ['./converter.component.scss']
})
export class ConverterComponent implements OnInit {
  public commandsForm!: FormGroup;
  public select$!: Observable<ResultModel>;
  public orderBy$!: Observable<ResultModel>;
  public groupBy$!: Observable<ResultModel>;
  public commands: any[] = [
    { name: 'SELECT' },
    { name: 'ORDER BY' },
    { name: 'GROUP BY' }
  ];

  constructor(private formBuilder: FormBuilder,
              private queryService: QueryService) { }

  ngOnInit(): void {
    this.commandsForm = this.formBuilder.group({
      command: ['', Validators.required],
      SQLQuery: ['', Validators.required]
    })
  }

  public sendCommand(): void {
    let command = this.commandsForm.controls['command'].value;
    let SQLQuery = this.commandsForm.controls['SQLQuery'].value;

    if (command == "SELECT") {
      console.log('select');
      this.select$ = this.queryService.ConvertSelectQuery(SQLQuery);
      let item;
      this.select$.subscribe( data => item = data);
      console.log(item);
    } else if (command == "ORDER BY") {
      console.log('order by');
      this.orderBy$ = this.queryService.ConvertOrderByQuery(SQLQuery);
    } else if (command == "GROUP BY") {
      console.log('group by');
      this.groupBy$ = this.queryService.ConvertGroupByQuery(SQLQuery);
    }
  }
}
