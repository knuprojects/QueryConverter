import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'jsonPipe'
})
export class JsonPipePipe implements PipeTransform {

  transform(value: any, ...args: any[]): any {
    return JSON.stringify(value, null, 2)
      //.replace(/ /, '<br/>') // note the usage of / /g instead of ' ' in order to replace all occurences
      //replace(/\r?\n|\r/g, '<br>')// same here
      //.replace(/\n/g, '<br/>'); // same here
  }

}
