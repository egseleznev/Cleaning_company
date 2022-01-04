import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth/auth.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {


  public msg: string;
  constructor(private _auth: AuthService ) { }
  ngOnInit(): void {
    this.ResetMsg();
  }

  public ResetMsg():void{
    this.msg = 'Войдите чтобы продолжить';
  }
  public Login(info: { username: string, password: string }) {
    this._auth.login(JSON.parse(JSON.stringify(info))).subscribe(
      status=>
      {
        if (status==200)
        {
          this.msg = 'Успешно';
          this._auth.sendTestRequest();
          window.location.href='http://localhost:4200/profile'
        }
        else if (status==401)
          this.msg = 'Неправильно введены данные';
        else
          this.msg = `Что-то пошло не так (${status})`;
      });
  }

}
