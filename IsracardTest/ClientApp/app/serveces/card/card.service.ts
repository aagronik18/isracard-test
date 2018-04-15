import { Inject, Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { CustomCard } from '../../components/app/app.component';

@Injectable()
export class CardService {

    private baseUrl: string = '/';
    private url: string = 'api/SampleData/CardsForcasts';
    private bookmarkSetUrl: string = 'api/SampleData/CardToSession';
    private bookmarkGetUrl: string = 'api/SampleData/CardFromsession';

    //public cards: any = [];

    constructor(private http: Http, @Inject('BASE_URL') baseUrl: string) {

        this.baseUrl = baseUrl;
    }

    public getCards(searchStr: string) {

        return this.http.get(this.baseUrl + this.url + '/' + searchStr);
    }

    public bookmarkCard(card: CustomCard) {

        return this.http.post(this.baseUrl + this.bookmarkSetUrl, card);
    }

    public bookmarkGet() {

        return this.http.get(this.baseUrl + this.bookmarkGetUrl);
    }
}