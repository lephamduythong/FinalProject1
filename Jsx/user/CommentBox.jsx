import React from 'react';
import ReactDOM from 'react-dom';
import CSSTransitionGroup from 'react-transition-group/CSSTransitionGroup';
import $ from 'jquery';

export default class CommentBox extends React.Component {
    constructor() {
        super();
        this.state = {
            idListComment : [],
        }
        this.sendComment = this.sendComment.bind(this);
    }
    
    sendComment(commentValue) {
        let thisCom = this;
        console.log(this.props.author);
        $.post("/Ajax/Comment", {code: 3, idBaiHoc: this.props.idBaiHoc, tacGia: this.props.author, noiDung: commentValue, hinh: this.props.authorImage}, function(data){
            console.log(data);
            let change = thisCom.state.idListComment;
            change.unshift({id: data});
            thisCom.setState({idListComment: change});
        });
    }

    render() {
        return (
            <div>
                <div>
                    <WriteComment
                        idBaiHoc = {this.props.idBaiHoc}
                        key={0}
                        ajaxURL={this.props.ajaxURL} 
                        sendComment={this.sendComment}
                        />
                </div>
                <br />
                <CSSTransitionGroup
                transitionName="example"
                transitionEnterTimeout={800}
                transitionLeaveTimeout={800}>
                    {
                        this.state.idListComment.map((obj,index) => {
                            return <Comment 
                                idBinhLuan={obj.id} 
                                key={obj.id}
                                ajaxURL={this.props.ajaxURL} />
                        })
                    }
                </CSSTransitionGroup>
                
            </div>
        )    
    }
    componentDidMount() {
        let thisCom = this;
        $.post("/Ajax/Comment", {code: 1, idBaiHoc: this.props.idBaiHoc}, function(data){
            console.log(data);
            thisCom.setState({idListComment: data})
        });
    }
}

class WriteComment extends React.Component {
    constructor() {
        super();

    }

    render() {
        return(
            <div className="writeComment-wrapper">
                <textarea ref="txtComment" style={{'width':'100%','height':'150px'}}></textarea>
                <br />
                <button onClick={() => {this.props.sendComment(this.refs.txtComment.value)}} className="btn btn-success">Gửi bình luận</button>
            </div>
        )
    }
    componentDidMount() {

    }
}

class Comment extends React.Component {
    constructor() {
        super();
        this.state = {
            author: "Loading...",
            hinh: '',
            comment: "Loading..."
        }
    }
    render() {
        return (
            <div className="comment-wrapper">
                <div className="author">
                    <img width="32" src={"/images/sample_avatars/" + this.state.hinh} />
                    &nbsp;
                    {this.state.author}
                </div>
                <div className="comment">
                    {this.state.comment}
                </div>
            </div>
        )
    }
    componentDidMount() {
        let thisCom = this;
        $.post("/Ajax/Comment", {code: 2, idBinhLuan: this.props.idBinhLuan}, function(data){
            console.log(data);
            thisCom.setState({
                author: data.tacGia,
                comment: data.noiDung,
                hinh: data.hinh,
            })
        });
    }
}

window.renderCommentBox = function(_id, _element, _author, _authorImage){
    ReactDOM.render(<CommentBox 
        idBaiHoc={_id} 
        author={_author}
        authorImage={_authorImage}
         />, document.getElementById(_element));
};

