import { Injectable } from '@angular/core';
import {CheckboxComponent} from "../components/check-box.component";

@Injectable({
  providedIn: 'root'
})

export class ConfigStaticService {
  static tableConfig = () => {
    return {
      id: {
        title: 'ID'
      },
      name: {
        title: 'Name'
      },
      address: {
        title: 'Address'
      },
      cuisine: {
        title: 'Cuisine'
      },
      familyFriendly: {
        title: 'Family Friendly',
        type: 'custom',
        renderComponent: CheckboxComponent,
        attr: {
          class: 'check-box'
        }
      },
      veganOptions: {
        title: 'Vegan Options',
        type: 'custom',
        renderComponent: CheckboxComponent,
        attr: {
          class: 'check-box'
        }
      },
      rating: {
        title: 'Rating'
      }
    };
  }
}
