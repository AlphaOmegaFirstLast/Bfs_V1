import { credits, currentYear } from '@/app/constants';
import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
    selector: 'bfs-error-401',
    imports: [RouterLink],
    templateUrl: './error-401.component.html',
    styles: ``
})
export class Error401Component {
    currentYear = currentYear
    credits = credits
}
