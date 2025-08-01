
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UUID } from 'crypto';
import { Observable } from 'rxjs';

export interface Coordinates {
  latitude: number;
  longitude: number;
}

export interface Vehicle {
  id?: UUID;
  identifier: string;
  chassis: string;
  licensePlate: string;
  trackerSerialNumber: string;
  vehicleType: number;
  color: string;
  coordinates: Coordinates;
}

@Injectable({
  providedIn: 'root'
})
export class VehicleService {
  private readonly apiUrl = 'http://localhost:5000/api/v1/Veiculos';

  constructor(private http: HttpClient) {}

  getVehicles(userLatitude: number, userLongitude: number): Observable<Vehicle[]> {
    return this.http.get<Vehicle[]>(`${this.apiUrl}/Listar?userLatitude=${userLatitude}&userLongitude=${userLongitude}`, { withCredentials: true });
  }

  createVehicle(vehicle: Vehicle): Observable<Vehicle> {
    return this.http.post<Vehicle>(`${this.apiUrl}/Cadastrar`, vehicle);
  }

  deleteVehicle(id: UUID): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/Remover`, {Id: id});
  }

  updateVehicle(vehicle: Vehicle): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/Atualizar`, vehicle);
  }
}