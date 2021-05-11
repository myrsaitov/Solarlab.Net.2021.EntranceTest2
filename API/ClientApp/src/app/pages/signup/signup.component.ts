import {Component, OnInit} from '@angular/core';
import {AccountService} from '../../services/account.service';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {confirmPasswordValidator} from '../../directives/confirm-password-validator.directive';
import {Router} from '@angular/router';
import {AuthService} from 'src/app/services/auth.service';
import {ApiUrls} from 'src/app/shared/apiURLs';
import {ILogin} from 'src/app/models/account/login.model';
import {BaseService} from 'src/app/services/base.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss']
})

export class SignupComponent implements OnInit {
  form: FormGroup;
  notregisterstatus = false;

  constructor(
    private fb: FormBuilder, 
    private accountService: AccountService,
    private readonly auth: AuthService,
    private readonly baseService: BaseService,
    private readonly router: Router) {
  }

  ngOnInit() {
    const pattern = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[+!@#$%^&*]).{6,20}/g;

    this.form = this.fb.group({
      userName: ['', [Validators.required,Validators.minLength(5), Validators.maxLength(50), Validators.pattern("[a-zA-Z0-9_]*")]],
      email: ['', [Validators.required, Validators.email]],
      firstName: ['', [Validators.minLength(1), Validators.maxLength(50), Validators.pattern("[A-Z][a-z]*")]],
      lastName: ['', [Validators.minLength(1), Validators.maxLength(50), Validators.pattern("[A-Z][a-z]*")]],
      middleName: ['', [Validators.minLength(1), Validators.maxLength(50), Validators.pattern("[A-Z][a-z]*")]],
      password: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(20), Validators.pattern(pattern)]],
      confirmPassword: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(20)]]
    }, {validators: confirmPasswordValidator});
  }

  get userName() {
    return this.form.get('userName');
  }

  get email() {
    return this.form.get('email');
  }

  get firstName() {
    return this.form.get('firstName');
  }

  get lastName() {
    return this.form.get('lastName');
  }
  
  get middleName() {
    return this.form.get('middleName');
  }

  get password() {
    return this.form.get('password');
  }

  get confirmPassword() {
    return this.form.get('confirmPassword');
  }


  public async register() {

    console.log("Called register()");
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }




 var RES = await this.accountService
  .register(this.form.value)
  .toPromise();

   

    //LOGIN
  if(RES)
  {
      this.form.markAllAsTouched();
      if (this.form.invalid) 
      {  
        return;
      }
    
      const payload: ILogin = this.form.getRawValue();
      localStorage.setItem('currentUser', payload.userName);
      await this.baseService.post(ApiUrls.login, payload)
        .then(res => {
          if (res) 
          {
            this.auth.saveSession(res);
            this.router.navigate(['/']);
          }
        });
  }
  else
  {
    this.notregisterstatus = true;
  }

}


  
}
