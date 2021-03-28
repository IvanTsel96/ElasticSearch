import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable()
export class ApiClient {
  private header: HttpHeaders;
  private responseType: 'json';
  private apiUrl: string = environment.apiUrl;

  constructor(private readonly httpClient: HttpClient) {
    this.header = new HttpHeaders().set('Content-type', 'application/json');
    this.responseType = 'json';
  }

  public getData<T>(
    url: string,
    params: any = null,
    responseType: 'json' = this.responseType
  ): Observable<T> {
    return this.httpClient.get<T>(this.apiUrl + url, {
      headers: this.header,
      params: params,
      responseType: responseType,
    });
  }

  public postData<T, U>(
    url: string,
    data: U,
    params: HttpParams = null
  ): Observable<T> {
    return this.httpClient.post<T>(this.apiUrl + url, data, {
      headers: this.header,
      params: params,
    });
  }

  public putData<T>(
    url: string,
    data: T,
    params: HttpParams = null
  ): Observable<T> {
    return this.httpClient.put<T>(this.apiUrl + url, data, {
      headers: this.header,
      params: params,
    });
  }

  public deleteData<T>(
    url: string,
    params: HttpParams = null
  ): Observable<any> {
    return this.httpClient.delete(this.apiUrl + url, {
      headers: this.header,
      params: params,
    });
  }
}
