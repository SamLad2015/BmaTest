import {Component, EventEmitter, Input, OnChanges, Output} from '@angular/core';
import {FormGroup} from '@angular/forms';
import {Cuisine, Restaurant} from "../../../shared/restaurant";
import {FormBuilderStaticService} from "../../../shared/formBuilder.static.service";
import {CuisineService} from "../../../shared/cuisine.service";
import {AddRestaurant, LoadRestaurants, UpdateRestaurant} from "../../../../store/actions";
import {NgRedux} from "@angular-redux/store";
import {IAppState} from "../../../../store/store";
import {RestaurantService} from "../../../shared/restaurant.service";
import * as _ from 'lodash';

@Component({
  selector: 'restaurant',
  templateUrl: './restaurant.component.html',
  styleUrls: ['./restaurant.component.css']
})
export class RestaurantComponent implements OnChanges {
  @Input()
  restaurant: Restaurant;
  @Output() cancelForm: EventEmitter<any> = new EventEmitter();
  form: FormGroup;
  cuisines: Cuisine[];
  constructor(private fbsService: FormBuilderStaticService,
              private cuisineService: CuisineService,
              private restaurantService: RestaurantService,
              private ngRedux: NgRedux<IAppState>) {
   this.fbsService = fbsService;
   cuisineService.GetAllCuisines().subscribe(cuisines => this.cuisines = cuisines.result);
  }

  ngOnChanges(): void {
    this.form = this.fbsService.getRestaurantForm(this.restaurant);
  }

  saveChanges() {
    const formId = this.restaurant.id;
    const restaurantFormValue = this.form.value;
    restaurantFormValue.id = formId;
    if (!formId) {
      this.restaurantService.CreateRestaurant(this.formatPayload(restaurantFormValue)).subscribe();
      this.ngRedux.dispatch(AddRestaurant(restaurantFormValue));
    } else {
      this.restaurantService.UpdateRestaurant(this.formatPayload(restaurantFormValue)).subscribe();
      this.ngRedux.dispatch(UpdateRestaurant(restaurantFormValue));
    }
    this.cancelForm.emit();
  }

  cancelChanges() {
    this.form.reset();
    this.cancelForm.emit();
  }

  addRemoveCuisineTag(c: Cuisine) {
    const isPresent = this.form.controls.cuisine.value.indexOf(c.name) > -1;
    if (isPresent) {
      this.form.controls.cuisine
        .setValue(this.form.controls.cuisine.value.filter(cu => cu !== c.name));
    } else {
      this.form.controls.cuisine.value.push(c.name);
    }
  }

  isTagSelected = (c: Cuisine) => {
    return this.form.controls.cuisine.value.indexOf(c.name) > -1;
  }

  private formatPayload(restaurant: Restaurant) {
    restaurant.cuisineTagIds = _.map(this.cuisines.filter(c => restaurant.cuisine.indexOf(c.name) > -1), 'id');
    return restaurant;
  }
}
