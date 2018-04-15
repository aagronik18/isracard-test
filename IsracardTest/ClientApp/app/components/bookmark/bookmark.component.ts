import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CardService } from '../../serveces/card/card.service';
import { CustomCard } from '../app/app.component';

@Component({
    selector: 'bookmark',
    templateUrl: './bookmark.component.html',
    providers: [CardService]
})
export class BookmarkComponent {

    public cards: any;

    constructor(private cardService: CardService) { }

    ngOnInit() {

        this.cards = [];

        this.cardService.bookmarkGet().subscribe(result => {
            this.cards = result.json() as CustomCard[];
        }, error => console.error(error));
    }
}