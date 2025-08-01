import { Component, Input, Output, EventEmitter, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { Vehicle } from '../vehicle.service';
import {
  provideFluentDesignSystem,
  fluentButton,
  fluentTextField,
  fluentSelect,
  fluentOption
} from '@fluentui/web-components';

provideFluentDesignSystem().register(
  fluentButton(),
  fluentTextField(),
  fluentSelect(),
  fluentOption()
);

@Component({
  selector: 'app-vehicle-form',
  standalone: true,
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css'],
  imports: [],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class VehicleFormComponent {
  @Input() vehicle: Partial<Vehicle> = {
    identifier: '',
    chassis: '',
    licensePlate: '',
    trackerSerialNumber: '',
    vehicleType: 1,
    color: '#000000',
    coordinates: { latitude: 0, longitude: 0 }
  };

  @Output() save = new EventEmitter<Partial<Vehicle>>();
  @Output() cancel = new EventEmitter<void>();

  onSubmit(event: Event): void {
    event.preventDefault();
    this.save.emit(this.vehicle);
  }

  generateIdentifier(): void {
    this.vehicle.identifier = `V${Math.floor(Math.random() * 10000).toString().padStart(4, '0')}`;
  }

  generateChassis(): void {
    const vinChars = 'ABCDEFGHJKLMNPRSTUVWXYZ0123456789';
    let vin = '';
    for (let i = 0; i < 17; i++) {
      vin += vinChars.charAt(Math.floor(Math.random() * vinChars.length));
    }
    this.vehicle.chassis = vin;
  }

  generateLicensePlate(): void {
    const letters = () => Array.from({length: 3}, () => String.fromCharCode(65 + Math.floor(Math.random() * 26))).join('');
    const numbers = () => Math.floor(1000 + Math.random() * 9000);
    this.vehicle.licensePlate = `${letters()}-${numbers()}`;
  }

  generateTrackerSerialNumber(): void {
    const chars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789';
    this.vehicle.trackerSerialNumber = Array.from({length: 6}, () => chars.charAt(Math.floor(Math.random() * chars.length))).join('');
  }

  generateColor(): void {
    this.vehicle.color = `#${Math.floor(Math.random()*16777215).toString(16).padStart(6, '0')}`;
  }

  generateVehicleType(): void {
    this.vehicle.vehicleType = Math.random() < 0.5 ? 1 : 2;
  }

  generateLatitude(): void {
    const lat = (Math.random() * 180 - 90).toFixed(6);
    if (this.vehicle.coordinates) {
      this.vehicle.coordinates.latitude = +lat;
    } else {
      this.vehicle.coordinates = { latitude: +lat, longitude: 0 };
    }
  }

  generateLongitude(): void {
    const lon = (Math.random() * 360 - 180).toFixed(6);
    if (this.vehicle.coordinates) {
      this.vehicle.coordinates.longitude = +lon;
    } else {
      this.vehicle.coordinates = { latitude: 0, longitude: +lon };
    }
  }

  onLatInput(event: any) {
    let value = event.target.value;

    if (/^-?\d*(\.\d*)?$/.test(value)) {
      if (value.startsWith('-.')) value = value.replace('-.' , '-0.');
      if (value.startsWith('.')) value = value.replace('.', '0.');
      event.target.value = value;
      this.vehicle.coordinates!.latitude = value;
    } else {
      event.target.value = this.vehicle.coordinates!.latitude ?? '';
    }
  }

  onLonInput(event: any) {
    let value = event.target.value;
    if (/^-?\d*(\.\d*)?$/.test(value)) {
      if (value.startsWith('-.')) value = value.replace('-.' , '-0.');
      if (value.startsWith('.')) value = value.replace('.', '0.');
      event.target.value = value;
      this.vehicle.coordinates!.longitude = value;
    } else {
      event.target.value = this.vehicle.coordinates!.longitude ?? '';
    }
  }
}
