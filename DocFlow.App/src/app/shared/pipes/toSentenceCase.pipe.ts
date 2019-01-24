import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'appToSentenceCase'
})
export class ToSentenceCasePipe implements PipeTransform {
    transform(value: string): string {
        return value.replace(/([a-z]+)([A-Z][a-z]+)/g, '$1 $2').trim();
    }
}
