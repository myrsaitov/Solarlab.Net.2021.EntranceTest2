import {Injectable} from '@angular/core';
import {BehaviorSubject} from 'rxjs';

@Injectable({providedIn: 'root'})

export class AuthService {
  private readonly tokenName = '  login_session';
  private isAuthSubject$: BehaviorSubject<boolean> = new BehaviorSubject(false);

  get isAuth() {
    return this.isAuthSubject$.value;
  }

  get isAuth$() {
    return this.isAuthSubject$.asObservable();
  }

  saveSession(token: string): void {
    localStorage.setItem(this.tokenName, JSON.parse(token).token);
    this.isAuthSubject$.next(!!this.getSession());
  }

  loadSession(): void {
    this.isAuthSubject$.next(!!this.getSession());
  }

  getSession(): string {
    return localStorage.getItem(this.tokenName);
  }

  deleteSession(): void {
    localStorage.removeItem(this.tokenName);
    this.isAuthSubject$.next(!!this.getSession());
  }

  getUserName() {

    return localStorage.getItem('currentUser');

  }

  isAuthenticated(): Promise<boolean> {
    return new Promise((resolve) => {
      resolve(!!this.getSession());
    });
  }
}
