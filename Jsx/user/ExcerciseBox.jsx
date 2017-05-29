import React from 'react';
import ReactDOM from 'react-dom';
import ReactCSSTransitionGroup from 'react-addons-css-transition-group';
import $ from 'jquery';
import _ from 'underscore';

var excerciseBox;

export default class ExcerciseBox extends React.Component {
    constructor() {
        super();
        this.state = {
            listQuestion: [],
            choosingAnswer: [],
            idQuestionIndex: 0,
            agentImageNumber: 1,
        }
        excerciseBox = this;
    }

    prevQuestion() {
        if (this.state.idQuestionIndex > 0) {
            let changeAgentImageNumber = 0;
            if (this.state.agentImageNumber == 4)
                changeAgentImageNumber = 1;
            else
                changeAgentImageNumber = this.state.agentImageNumber + 1;

            this.setState({ idQuestionIndex: this.state.idQuestionIndex - 1, agentImageNumber: changeAgentImageNumber });
        }
    }

    nextQuestion() {
        if (this.state.idQuestionIndex < this.state.listQuestion.length - 1) {
            let changeAgentImageNumber = 0;
            if (this.state.agentImageNumber == 4)
                changeAgentImageNumber = 1;
            else
                changeAgentImageNumber = this.state.agentImageNumber + 1;

            this.setState({ idQuestionIndex: this.state.idQuestionIndex + 1, agentImageNumber: changeAgentImageNumber });
        }

    }

    chooseAnswer(number) {
        let array = this.state.choosingAnswer;
        array[this.state.idQuestionIndex] = number;
        this.setState({ choosingAnswer: array });
    }

    submit() {
        if (confirm('Em có muốn nộp bài ?')) {
            $.ajaxSetup({ async: false });
            let thisCom = this;
            let listIdQuestion = [];
            for (let i = 0; i < this.state.listQuestion.length; i++) {
                listIdQuestion.push(this.state.listQuestion[i].id);
            }
            $.post("/Ajax/Excercise", {
                code: 2,
                listIdQuestion: listIdQuestion,
                choosingAnswer: this.state.choosingAnswer,
                idHocSinh: this.props.idHocSinh,
                idBaiTap: this.props.idBaiTap
            }, function (data) {
                // console.log(data);
                alert("Em hoàn thành được " + data + "% trên tổng số câu hỏi");
                window.location.href = `/`;
            });
            $.ajaxSetup({ async: true });
        }
    }

    render() {
        // console.log(_.propertyOf(this.state.listQuestion[this.state.idQuestionIndex])("noiDung"));
        return (
            <div className="excerciseBox-wrapper">
                <div className="row">
                    <div className="col-xs-12 col-sm-3">
                        <div className="agent-image">
                            <img src={"/images/question/" + this.state.agentImageNumber + ".jpg"} />
                        </div>
                    </div>
                    <div dangerouslySetInnerHTML={{ __html: _.unescape(_.propertyOf(this.state.listQuestion[this.state.idQuestionIndex])("noiDung")) }} className="col-xs-12 col-sm-9 question">
                    </div>
                </div>
                <div className="row">
                    <div className="col-xs-12">
                        <div className="answers">
                            <label>
                                <input onChange={() => { this.chooseAnswer(1) }}
                                    className="option-input radio"
                                    type="radio"
                                    checked={this.state.choosingAnswer[this.state.idQuestionIndex] == 1 ? "checked" : ""}
                                    name={this.state.idQuestionIndex}
                                />
                                &nbsp;&nbsp;
                                {_.propertyOf(this.state.listQuestion[this.state.idQuestionIndex])("cauTraLoi1")}
                            </label>
                            <br />
                            <label>
                                <input onChange={() => { this.chooseAnswer(2) }}
                                    className="option-input radio"
                                    type="radio"
                                    checked={this.state.choosingAnswer[this.state.idQuestionIndex] == 2 ? "checked" : ""}
                                    name={this.state.idQuestionIndex} />
                                &nbsp;&nbsp;
                                {_.propertyOf(this.state.listQuestion[this.state.idQuestionIndex])("cauTraLoi2")}
                            </label>
                            <br />
                            <label>
                                <input onChange={() => { this.chooseAnswer(3) }}
                                    className="option-input radio"
                                    type="radio"
                                    checked={this.state.choosingAnswer[this.state.idQuestionIndex] == 3 ? "checked" : ""}
                                    name={this.state.idQuestionIndex} />
                                &nbsp;&nbsp;
                                {_.propertyOf(this.state.listQuestion[this.state.idQuestionIndex])("cauTraLoi3")}
                            </label>
                            <br />
                            <label>
                                <input onChange={() => { this.chooseAnswer(4) }}
                                    className="option-input radio"
                                    type="radio"
                                    checked={this.state.choosingAnswer[this.state.idQuestionIndex] == 4 ? "checked" : ""}
                                    name={this.state.idQuestionIndex} />
                                &nbsp;&nbsp;
                                {_.propertyOf(this.state.listQuestion[this.state.idQuestionIndex])("cauTraLoi4")}
                            </label>
                        </div>
                    </div>
                </div>
                <div className="row">
                    <div className="col-xs-12">
                        <div className="exercise-buttons">
                            <br />
                            <button onClick={() => { this.prevQuestion() }} className="btn btn-primary" id="ex-prev">Trước</button>
                            <button onClick={() => { this.nextQuestion() }} className="btn btn-primary" id="ex-next">Sau</button>
                        </div>
                    </div>
                </div>

                {
                    this.state.idQuestionIndex == this.state.listQuestion.length - 1 ?
                        <div className="row">
                            <br />
                            <div className="col-xs-12">
                                <div className="exercise-buttons">
                                    <button onClick={() => { this.submit() }} className="popup-with-zoom-anim btn btn-warning">Nộp bài</button>
                                </div>
                            </div>
                        </div>
                        : ""
                }
            </div>
        )
    }
    componentDidMount() {
        let thisCom = this;
        $.post("/Ajax/Excercise", { code: 1, idBaiTap: this.props.idBaiTap }, function (data) {
            console.log(data);
            let arraySize = data.length;
            let pushArray = [];
            while (arraySize--)
                pushArray.push(-1);
            thisCom.setState({ listQuestion: data, choosingAnswer: pushArray });
        });
    }
}


window.renderExcerciseBox = function (_idBaiTap, _idHocSinh, _element) {
    ReactDOM.render(<ExcerciseBox
        idBaiTap={_idBaiTap}
        idHocSinh={_idHocSinh}
         />, document.getElementById(_element), function () {
            $(document).keydown(function (e) {
                if (e.keyCode == 37) {
                    e.preventDefault();
                    excerciseBox.prevQuestion();
                }
                if (e.keyCode == 39) {
                    e.preventDefault();
                    excerciseBox.nextQuestion();
                }
            });
        });
};