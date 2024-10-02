import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'http://localhost:5284/api/auth';

  constructor(private http: HttpClient) {}

  register(username: string, password: string, confirmPassword: string): Observable<any> {
    const body = { username, password, confirmPassword };
    return this.http.post(`${this.apiUrl}/register`, body);
  }

  login(username: string, password: string): Observable<any> {
    const body = { username, password };
    return this.http.post(`${this.apiUrl}/login`, body).pipe(
      tap((response: any) => {
        localStorage.setItem('access_token', response.accessToken);
      })
    );
  }

  logout() {
    localStorage.removeItem('access_token');
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('access_token');
  }

  getToken(): string | null {
    return localStorage.getItem('access_token');
  }
}