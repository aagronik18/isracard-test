import { Component } from '@angular/core';
import { CardService } from '../../serveces/card/card.service';
import { CustomCard } from '../app/app.component';

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    providers: [CardService]
})
export class HomeComponent {

    public cards: any;

    constructor(private cardService: CardService) { }

    ngOnInit() {

        this.cards = [];
    }

    public onSearchForString(event: any) {

        this.cardService.getCards(event).subscribe(result => {
            this.cards = result.json() as CustomCard[];
        }, error => console.error(error));
    }

    public onBookmarkCard(event: any) {

        this.cardService.bookmarkCard(event).subscribe();
    }
}