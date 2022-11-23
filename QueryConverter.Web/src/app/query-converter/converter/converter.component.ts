import {Component, OnDestroy, OnInit} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import {Observable, Subscription} from "rxjs";

import { HttpService } from "../../core/services/http-service";
import { ResultModel } from "../../core/models/result-model";
import { CommandModel } from "../../core/models/command-model";

@Component({
  selector: 'app-converter',
  templateUrl: './converter.component.html',
  styleUrls: ['./converter.component.scss']
})
export class ConverterComponent implements OnInit, OnDestroy {
  private subscriptions: Array<Subscription> = [];
  public commandsForm!: FormGroup;
  public select$!: Observable<ResultModel>;
  public orderBy$!: Observable<ResultModel>;
  public groupBy$!: Observable<ResultModel>;
  public jsonResult: any;
  public isJsonResult: boolean = false;
  public commands: any[] = [
    { name: 'SELECT' },
    { name: 'ORDER BY' },
    { name: 'GROUP BY' }
  ];

  constructor(private formBuilder: FormBuilder,
              private httpService: HttpService) { }

  ngOnInit(): void {
    this.commandsForm = this.formBuilder.group({
      command: ['', Validators.required],
      SQLQuery: ['', Validators.required]
    })
  }

  public sendCommand(): void {
    this.isJsonResult = true;
    let command = this.commandsForm.controls['command'].value;
    let SQLQuery = this.commandsForm.controls['SQLQuery'].value;
    let commandModel = new CommandModel(SQLQuery);

    if (command == "SELECT") {
      this.select$ = this.httpService.ConvertQuery(commandModel, 'select');
      this.select$.subscribe(x => {
        this.jsonResult = x.elasticQuery.replace(/(?:\r\n|\r|\n)/g, '<br>');
        this.subscriptions.push(this.jsonResult);
      });
    } else if (command == "ORDER BY") {
      this.orderBy$ = this.httpService.ConvertQuery(commandModel, 'orderBy');
      this.orderBy$.subscribe(x => {
        this.jsonResult = x.elasticQuery.replace(/(?:\r\n|\r|\n)/g, '<br>');
        this.subscriptions.push(this.jsonResult);
      });
    } else if (command == "GROUP BY") {
      this.groupBy$ = this.httpService.ConvertQuery(commandModel, 'groupBy');
      this.groupBy$.subscribe(x => {
        this.jsonResult = x.elasticQuery.replace(/(?:\r\n|\r|\n)/g, '<br>');
        this.subscriptions.push(this.jsonResult);
      });
    }
  }

  ngOnDestroy() {
    for (const subs of this.subscriptions) {
      subs.unsubscribe();
    }
  }
}
