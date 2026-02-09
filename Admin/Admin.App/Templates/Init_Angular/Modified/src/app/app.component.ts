import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, NavigationEnd, Router, RouterOutlet } from '@angular/router';
import * as tablerIcons from '@ng-icons/tabler-icons';
import * as tablerIconsFill from '@ng-icons/tabler-icons/fill';
import { provideIcons } from '@ng-icons/core';
import { Title } from '@angular/platform-browser';
import { filter, map, mergeMap } from 'rxjs/operators';

@Component({
    selector: 'app-root',
    imports: [RouterOutlet, CommonModule],
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss',
    viewProviders: [provideIcons({ ...tablerIcons, ...tablerIconsFill })]
})
export class AppComponent implements OnInit {
    private titleService: Title;
    private router: Router;
    private activatedRoute: ActivatedRoute;
    isLoading = true;
    constructor() {
        this.titleService = inject(Title);
        this.router = inject(Router);
        this.activatedRoute = inject(ActivatedRoute);
    }

    //--------------------------------------------------------------------------------------------
    ngOnInit(): void {
        this.router.events
            .pipe(
                filter(event => event instanceof NavigationEnd),
                map(() => {
                    let route = this.activatedRoute;
                    while (route.firstChild) {
                        route = route.firstChild;
                    }
                    return route;
                }),
                mergeMap(route => route.data)
            )
            .subscribe(data => {
                this.titleService.setTitle('BestFit Solutions');
                this.isLoading = false;
            });
    }
}
