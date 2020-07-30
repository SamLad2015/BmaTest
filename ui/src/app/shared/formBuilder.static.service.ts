import {FormBuilder, Validators} from '@angular/forms';
import {Restaurant} from "./restaurant";
import {Injectable} from "@angular/core";

@Injectable({
  providedIn: 'root'
})

export class FormBuilderStaticService {
  constructor(private fb: FormBuilder) { }
  getRestaurantForm = (restaurant: Restaurant) => {
    return this.fb.group({
      name: [restaurant && restaurant.name ? restaurant.name : '', Validators.required],
      address: [restaurant && restaurant.address ? restaurant.address : '', Validators.required],
      cuisine: [restaurant && restaurant.cuisine ? restaurant.cuisine : [], Validators.required],
      familyFriendly: [restaurant && restaurant.familyFriendly ? restaurant.familyFriendly : false, Validators.required],
      veganOptions: [restaurant && restaurant.veganOptions ? restaurant.veganOptions : false, Validators.required],
      rating: [restaurant && restaurant.rating ? restaurant.rating : '', Validators.required]
    });
  }
}
