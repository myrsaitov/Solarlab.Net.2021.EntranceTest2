import {Component, Input} from '@angular/core';
import {IComment} from '../../models/comment/i-comment';

@Component({
  selector: 'app-comment-card',
  templateUrl: './comment-card.component.html',
  styleUrls: ['./comment-card.component.scss']
})

export class CommentCardComponent {
  @Input() comment: IComment;
}