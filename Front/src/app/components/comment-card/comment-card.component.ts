import { Component, Input } from '@angular/core';
import { IComment } from '../../models/comment/i-comment';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CommentService } from '../../services/comment.service';
import { AuthService } from '../../services/auth.service';
import { ToastService } from '../../services/toast.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-comment-card',
  templateUrl: './comment-card.component.html',
  styleUrls: ['./comment-card.component.scss']
})

export class CommentCardComponent {
  form: FormGroup;
  @Input() comment: IComment;

  constructor(private fb: FormBuilder,
    private authService: AuthService,
    private toastService: ToastService,
    private router: Router,
    private commentService: CommentService) {
}

delete_comment(){
  this.commentService.delete(1).pipe().subscribe(() => {
    this.toastService.show('Комментарий успешено удален', {classname: 'bg-success text-light'});
 

    });
}

getContentByUserName(userName: string){
  this.router.navigate(['/'], { queryParams: { userName: userName } });
}

}