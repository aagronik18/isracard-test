import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CustomCard } from '../app/app.component';

@Component({
    selector: 'card',
    templateUrl: './card.component.html'
})
export class CardComponent {

    @Input() card: any;
    @Output() customEvent: EventEmitter<CustomCard> = new EventEmitter<CustomCard>()

    public onClick(event: any) {

        this.customEvent.emit(this.card);
    }
}