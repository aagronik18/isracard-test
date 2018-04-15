import { Component, Output, EventEmitter } from '@angular/core';

@Component({
    selector: 'search',
    templateUrl: './search.component.html'
})
export class SearchComponent {

    public values: string = '';
    @Output() customEvent: EventEmitter<string> = new EventEmitter<string>();

    public onKey(event: any) { 

        if (event.keyCode == 13) {
            this.customEvent.emit(this.values);
            this.values = '';
        } else
            this.values = event.target.value;
    }
}