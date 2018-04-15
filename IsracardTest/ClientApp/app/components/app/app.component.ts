import { Component } from '@angular/core';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
})
export class AppComponent {
}

export interface CustomCard {
    Id: number;
    Title: string;
    Description: string;
    OwnerAvatarUrl: string;
}