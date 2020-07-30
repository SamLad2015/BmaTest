import {AfterViewInit, Component, OnInit} from '@angular/core';
import {Cuisine, Filters} from "../../../shared/restaurant";
import {CuisineService} from "../../../shared/cuisine.service";
import {animate, state, style, transition, trigger} from "@angular/animations";
import {LoadRestaurants} from "../../../../store/actions";
import {RestaurantService} from "../../../shared/restaurant.service";
import {NgRedux} from "@angular-redux/store";
import {IAppState} from "../../../../store/store";

@Component({
  selector: 'filters',
  templateUrl: './filters.component.html',
  styleUrls: ['./filters.component.css'],
  animations: [
    trigger('changeDivSize', [
      state('initial', style({
        height: '20px',
        width: '200px'
      })),
      state('final', style({
        height: 'auto',
        width: '100%',
        borderBottom: '3px',
        borderStyle: 'solid',
        borderColor: '#333'
      })),
      transition('initial=>final', animate('300ms')),
      transition('final=>initial', animate('300ms'))
    ]),
  ]
})
export class FiltersComponent {
  filters: Filters;
  cuisines: Cuisine[];
  isOpen: boolean;
  showFilters: boolean;
  constructor(private cuisineService: CuisineService,
              private restaurantService: RestaurantService,
              private ngRedux: NgRedux<IAppState>) {
    this.filters = new Filters();
  }

  addRemoveCuisineTag(c: Cuisine) {
    const isPresent = this.filters.cuisineTagIds.indexOf(c.id) > -1;
    if (isPresent) {
      this.filters.cuisineTagIds = this.filters.cuisineTagIds.filter(cu => cu !== c.id);
    } else {
      this.filters.cuisineTagIds.push(c.id);
    }
  }

  isTagSelected = (c: Cuisine) => {
    return this.filters.cuisineTagIds.indexOf(c.id) > -1;
  }

  applyFilters() {
    this.restaurantService
      .GetAllRestaurants(this.formatRequest(this.filters))
      .subscribe((data) => {
        this.ngRedux.dispatch(LoadRestaurants(data.result));
        this.isOpen = false;
        this.showFilters =  false;
      });
  }

  cancelModal() {
    this.filters = new Filters();
    this.isOpen = false;
    this.showFilters =  false;
  }

  loadTags(event: any) {
    if (event.toState === 'final') {
      this.cuisineService.GetAllCuisines()
        .subscribe(cuisines => this.cuisines = cuisines.result);
      this.showFilters = true;
    }
  }

  formatRequest = (request: Filters) => {
    if (request.familyFriendly && request.familyFriendly.toString() === 'both') {
      delete request.familyFriendly;
    }
    if (request.veganOptions  && request.veganOptions.toString() === 'both') {
      delete request.veganOptions;
    }
    return request;
  }

  resetFilters() {
    this.filters = new Filters();
    this.restaurantService
      .GetAllRestaurants()
      .subscribe((data) => {
        this.ngRedux.dispatch(LoadRestaurants(data.result));
      });
  }
}
