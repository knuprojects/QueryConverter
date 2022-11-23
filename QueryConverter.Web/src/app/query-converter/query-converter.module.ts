import { NgModule } from '@angular/core';
import { ConverterComponent } from './converter/converter.component';
import { QueryConverterRoutingModule } from "./query-converter-routing.module";
import { MatToolbarModule } from "@angular/material/toolbar";
import { MatSelectModule } from "@angular/material/select";
import { MatInputModule } from "@angular/material/input";
import { MatButtonModule } from "@angular/material/button";
import { FormsModule, ReactiveFormsModule} from "@angular/forms";
import { CommonModule } from "@angular/common";

@NgModule({
  declarations: [
    ConverterComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    QueryConverterRoutingModule,
    MatToolbarModule,
    MatSelectModule,
    MatInputModule,
    MatButtonModule,
  ],
  providers: [],
})
export class QueryConverterModule { }
