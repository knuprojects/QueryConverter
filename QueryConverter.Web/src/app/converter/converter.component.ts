import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { QueryService } from "../services/query.service";
import {map, Observable} from "rxjs";
import { ResultModel } from "../models/resultModel";
import { CommandModel } from "../models/commandModel";

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
  public res: any;
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
    let commandModel = new CommandModel(SQLQuery);

    if (command == "SELECT") {

      this.select$ = this.queryService.ConvertSelectQuery(commandModel);
      this.select$.subscribe(x => {console.log(x),
        this.res = x})
    } else if (command == "ORDER BY") {

      this.orderBy$ = this.queryService.ConvertOrderByQuery(commandModel);
      this.orderBy$.subscribe(x => {console.log(x),
        this.res = x})
    } else if (command == "GROUP BY") {

      this.groupBy$ = this.queryService.ConvertGroupByQuery(commandModel);
      this.groupBy$.subscribe(x => {console.log(x),
        this.res = x})
    }
  }
}