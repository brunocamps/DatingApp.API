import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Input() valuesFromHome: any; // in order to receive values from chd component
  // add output property to our register component
  @Output() cancelRegister = new EventEmitter();
  model: any = {};


  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  register() {
    // console.log(this.model);
    this.authService.register(this.model).subscribe(() => {
      console.log('registration successful!');
    }, error => {
      console.log(error); // http response back from the server
    });
  }

  cancel() {
    this.cancelRegister.emit(false);
    console.log('cancelled');
  }

}
