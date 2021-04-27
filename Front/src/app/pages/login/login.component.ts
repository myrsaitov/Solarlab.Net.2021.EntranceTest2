import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ILogin} from 'src/app/models/account/login.model';
import {BaseService} from 'src/app/services/base.service';
import {ApiUrls} from 'src/app/shared/apiURLs';
import {Router} from '@angular/router';
import {AuthService} from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  public loginForm: FormGroup;
  notloginedstatus = false;

  private formObj = {
    userName: [null, [Validators.required, Validators.pattern("[a-zA-Z0-9_]*")]],
    password: [null, [Validators.required, Validators.min(8)]],
    rememberMe: [false]
  };

  constructor(
    private readonly baseService: BaseService,
    private readonly router: Router,
    private readonly auth: AuthService,
    private fb: FormBuilder,
  ) {
    this.loginForm = fb.group(this.formObj);
  }

  ngOnInit() {

  }

  get userName() {
    return this.loginForm.get('userName');
  }

  get password() {
    return this.loginForm.get('password');
  }

  public async login() {
    this.loginForm.markAllAsTouched();
    if (this.loginForm.invalid) {
      return;
    }
    const payload: ILogin = this.loginForm.getRawValue();

    sessionStorage.setItem('currentUser', payload.userName);
    console.log(sessionStorage.getItem('currentUser'));
   
    await this.baseService.post(ApiUrls.login, payload)
      .then(res => {
        if (res) {
          this.auth.saveSession(res);
          this.router.navigate(['/']);
        }
          else{this.notloginedstatus = true;}
      });
}




      
  
}
