/* tslint:disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpResponse, HttpHeaders } from '@angular/common/http';
import { BaseService as __BaseService } from '../base-service';
import { ApiConfiguration as __Configuration } from '../api-configuration';
import { StrictHttpResponse as __StrictHttpResponse } from '../strict-http-response';
import { Observable as __Observable } from 'rxjs';
import { map as __map, filter as __filter } from 'rxjs/operators';

import { ReservationModel } from '../models/reservation-model';
import { Slot } from '../models/slot';
@Injectable({
  providedIn: 'root',
})
class ReservationService extends __BaseService {
  static readonly getApiReservationListPath = '/api/Reservation/List';
  static readonly getApiReservationFreeSlotsPath = '/api/Reservation/FreeSlots';
  static readonly postApiReservationDeletePath = '/api/Reservation/Delete';
  static readonly postApiReservationCreatePath = '/api/Reservation/Create';

  constructor(
    config: __Configuration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * Return all reservations for the specific day
   * @param day DateTime
   * @return When everything is OK
   */
  getApiReservationListResponse(day?: string): __Observable<__StrictHttpResponse<Array<ReservationModel>>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (day != null) __params = __params.set('day', day.toString());
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/Reservation/List`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<Array<ReservationModel>>;
      })
    );
  }
  /**
   * Return all reservations for the specific day
   * @param day DateTime
   * @return When everything is OK
   */
  getApiReservationList(day?: string): __Observable<Array<ReservationModel>> {
    return this.getApiReservationListResponse(day).pipe(
      __map(_r => _r.body as Array<ReservationModel>)
    );
  }

  /**
   * Return all available slots
   * @param day DateTime
   * @return When everything is OK
   */
  getApiReservationFreeSlotsResponse(day?: string): __Observable<__StrictHttpResponse<Array<Slot>>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (day != null) __params = __params.set('day', day.toString());
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/Reservation/FreeSlots`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<Array<Slot>>;
      })
    );
  }
  /**
   * Return all available slots
   * @param day DateTime
   * @return When everything is OK
   */
  getApiReservationFreeSlots(day?: string): __Observable<Array<Slot>> {
    return this.getApiReservationFreeSlotsResponse(day).pipe(
      __map(_r => _r.body as Array<Slot>)
    );
  }

  /**
   * Delete the specific reseration
   * @param body ReservationModel
   */
  postApiReservationDeleteResponse(body?: ReservationModel): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = body;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/Reservation/Delete`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<null>;
      })
    );
  }
  /**
   * Delete the specific reseration
   * @param body ReservationModel
   */
  postApiReservationDelete(body?: ReservationModel): __Observable<null> {
    return this.postApiReservationDeleteResponse(body).pipe(
      __map(_r => _r.body as null)
    );
  }

  /**
   * Create a new reservation
   * @param body
   */
  postApiReservationCreateResponse(body?: ReservationModel): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = body;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/Reservation/Create`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<null>;
      })
    );
  }
  /**
   * Create a new reservation
   * @param body
   */
  postApiReservationCreate(body?: ReservationModel): __Observable<null> {
    return this.postApiReservationCreateResponse(body).pipe(
      __map(_r => _r.body as null)
    );
  }
}

module ReservationService {
}

export { ReservationService }
