import React from 'react';
import ReactDOM from 'react-dom';
import CSSTransitionGroup from 'react-transition-group/CSSTransitionGroup';
import $ from 'jquery';

export const _TOKEN = $('input[name="__RequestVerificationToken"]').val();

export default class QuanLyChuong extends React.Component {
    constructor() {
        super();
        this.state = {
            idListChuongLop1: [],
            idListChuongLop2: [],
            idListChuongLop3: [],
            idListChuongLop4: [],
            idListChuongLop5: [],
        };
        this.delete = this.delete.bind(this)
    }

    add(lop) {
        // let randomId = Math.floor((Math.random() * 100) + 20);
        // console.log(randomId);
        let thisCom = this;
        let txtThemChuong = '';
        switch (lop) {
            case 1:
                txtThemChuong = this.refs.txtThemChuong1;
                break;
            case 2:
                txtThemChuong = this.refs.txtThemChuong2;
                break;
            case 3:
                txtThemChuong = this.refs.txtThemChuong3;
                break;
            case 4:
                txtThemChuong = this.refs.txtThemChuong4;
                break;
            case 5:
                txtThemChuong = this.refs.txtThemChuong5;
                break;
            default:
                break;
        }

        $.post('/Admin/QuanLyBaiHoc', { code: 5, __RequestVerificationToken: _TOKEN, idLop: lop, ten: txtThemChuong.value }, function (data) {
            console.log(data);
            // data: last id inserted
            switch (lop) {
                case 1:
                    thisCom.state.idListChuongLop1.unshift({ id: data });
                    thisCom.setState({ idListChuongLop1: thisCom.state.idListChuongLop1 });
                    break;
                case 2:
                    thisCom.state.idListChuongLop2.unshift({ id: data });
                    thisCom.setState({ idListChuongLop2: thisCom.state.idListChuongLop2 });
                    break;
                case 3:
                    thisCom.state.idListChuongLop3.unshift({ id: data });
                    thisCom.setState({ idListChuongLop2: thisCom.state.idListChuongLop3 });
                    break;
            }
        });
    }

    delete(idLop, idChuong) {
        // console.log(this);
        // console.log(idLop);
        // console.log(idChuong);
        $.post('/Admin/QuanLyBaiHoc', { code: 6, __RequestVerificationToken: _TOKEN, idChuong: idChuong }, function (data) {
            console.log(data);
        });


        let idListChuongLop = null;

        switch (idLop) {
            case 1:
                idListChuongLop = this.state.idListChuongLop1;
                break;
            case 2:
                idListChuongLop = this.state.idListChuongLop2;
                break;
            case 3:
                idListChuongLop = this.state.idListChuongLop3;
                break;
        }
        // console.log(idListChuongLop);

        let index = -1;
        for (let i = 0; i < idListChuongLop.length; i++) {
            if (idListChuongLop[i].id == idChuong) {
                index = i;
                break;
            }
        }

        // console.log(index);
        // Comment
        idListChuongLop.splice(index, 1);
        switch (idLop) {
            case 1:
                this.setState({ idListChuongLop1: idListChuongLop });
                break;
            case 2:
                this.setState({ idListChuongLop2: idListChuongLop });
                break;
            case 3:
                this.setState({ idListChuongLop3: idListChuongLop });
                break;
        }
    }

    render() {
        return (
            <div>
                <div className="lop-wrapper">
                    <h2>Lớp 1</h2>
                    <input type="text" ref="txtThemChuong1" />
                    <button className="btn btn-warning" onClick={() => this.add(1)}>Thêm một chương</button>
                    <CSSTransitionGroup 
                    transitionName="example"
                    transitionEnterTimeout={800}
                    transitionLeaveTimeout={800}>
                        {
                            this.state.idListChuongLop1.map((obj, index) => {
                                return (<Chuong key={obj.id}
                                    idChuong={obj.id}
                                    idLop="1"
                                    delete={this.delete} />)
                            })
                        }
                    </CSSTransitionGroup>
                </div>
                
                <div className="lop-wrapper">
                    <h2>Lớp 2</h2>
                    <input type="text" ref="txtThemChuong2" />
                    <button className="btn btn-warning" onClick={() => this.add(2)}>Thêm một chương</button>
                    <CSSTransitionGroup
                    transitionName="example"
                    transitionEnterTimeout={800}
                    transitionLeaveTimeout={800}>
                        {
                            this.state.idListChuongLop2.map((obj, index) => {
                                return (<Chuong key={obj.id}
                                    idChuong={obj.id}
                                    idLop="2"
                                    delete={(idLop, idChuong) => this.delete(idLop, idChuong)} />)
                            })
                        }
                    </CSSTransitionGroup>
                </div>

                <div className="lop-wrapper">
                    <h2>Lớp 3</h2>
                    <input type="text" ref="txtThemChuong3" />
                    <button className="btn btn-warning" onClick={() => this.add(3)}>Thêm một chương</button>
                    <CSSTransitionGroup
                        transitionName="example"
                        transitionEnterTimeout={800}
                        transitionLeaveTimeout={800}>
                        {
                            this.state.idListChuongLop3.map((obj, index) => {
                                return (<Chuong key={obj.id}
                                    idChuong={obj.id}
                                    idLop="3"
                                    delete={(idLop, idChuong) => this.delete(idLop, idChuong)} />)
                            })
                        }
                    </CSSTransitionGroup>
                </div>
            </div>
        )
    }

    componentDidMount() {
        let thisCom = this;
        $.post('/Admin/QuanLyBaiHoc', { code: 1, __RequestVerificationToken: _TOKEN }, function (data) {
            console.log(data);
            thisCom.setState({
                idListChuongLop1: data[0],
                idListChuongLop2: data[1],
            });
        });
    }
}

class Chuong extends React.Component {
    constructor() {
        super();
        // console.log(props);
        this.state = {
            ten: 'Chưa có tên',
            idListBaiHoc: [],
        }
        this.add.bind(this);
    }

    add() {
        console.log("Add");

        let ten = this.refs.txtThemBaiHoc.value;
        let idChuong = this.props.idChuong;

        let thisCom = this;
        $.post('/Admin/QuanLyBaiHoc', { code: 7, __RequestVerificationToken: _TOKEN, ten: ten, idChuong: idChuong }, function (data) {
            console.log(data);
            thisCom.state.idListBaiHoc.unshift({ id: data });
            thisCom.setState(thisCom.state.idListBaiHoc);
        });
    }

    delete(idBaiHoc) {
        let index = -1;
        console.log(idBaiHoc);
        let idBaiHocToDelete = idBaiHoc;

        $.post('/Admin/QuanLyBaiHoc', { code: 8, __RequestVerificationToken: _TOKEN, idBaiHoc: idBaiHocToDelete }, function (data) {
            console.log(data);
        });

        for (let i = 0; i < this.state.idListBaiHoc.length; i++) {
            if (this.state.idListBaiHoc[i].id == idBaiHoc) {
                index = i;
                break;
            }
        }

        this.state.idListBaiHoc.splice(index, 1);
        this.setState(this.state);
    }

    render() {
        return (
            <div className="chuong-wrapper">
                <h3>Chương: {this.state.ten} </h3>
                <button className="btn btn-primary" onClick={(idLop, idChuong) => { this.props.delete(parseInt(this.props.idLop), this.props.idChuong) }}>Xóa chương</button>
                <br />
                <input type="text" ref="txtThemBaiHoc" />
                <button className="btn btn-warning" onClick={() => this.add()}>Thêm một bài học</button>
                <CSSTransitionGroup
                    transitionName="example"
                    transitionEnterTimeout={800}
                    transitionLeaveTimeout={800}>
                    {
                        this.state.idListBaiHoc.map((obj, index) => {
                            return <BaiHoc
                                key={obj.id}
                                idChuong={this.props.idChuong}
                                idBaiHoc={obj.id}
                                delete={this.delete.bind(this)} />
                        })

                    }
                </CSSTransitionGroup>
            </div>
        )
    }
    componentDidMount() {
        let thisCom = this;
        $.post('/Admin/QuanLyBaiHoc', { code: 2, __RequestVerificationToken: _TOKEN, idChuong: this.props.idChuong, }, function (data) {
            // console.log(data);
            thisCom.setState({ ten: data.ten, idListBaiHoc: data.idListBaiHoc });
        });
    }
}

class BaiHoc extends React.Component {
    constructor() {
        super();
        this.state = {
            ten: 'Tên bài học',
            idListBaiTap: [],
        }
        this.delete = this.delete.bind(this);
    }
    
    add() {
        let thisCom = this;
        $.post('/Admin/QuanLyBaiTap', { code: 4, __RequestVerificationToken: _TOKEN, tenBaiTap: this.refs.txtThemBaiTap.value, idBaiHoc: this.props.idBaiHoc }, function (data) {
            console.log(data);
            let array = thisCom.state.idListBaiTap
            array.unshift({id: parseInt(data)});
            thisCom.setState({idListBaiTap: array});
        });
    }

    delete(idBaiTap) {
        let thisCom = this;
        $.post('/Admin/QuanLyBaiTap', { code: 5, __RequestVerificationToken: _TOKEN, idBaiTap: idBaiTap }, function (data) {
            console.log(data);
            let index = -1;
            for (let i = 0; i < thisCom.state.idListBaiTap.length; i++) {
            if (thisCom.state.idListBaiTap[i].id == idBaiTap) {
                    index = i;
                    break;
                }
            }
            thisCom.state.idListBaiTap.splice(index, 1);
            thisCom.setState(thisCom.state);
        });
    }

    render() {
        return (
            <div className="baihoc-wrapper">
                <h4>{this.state.ten}</h4>
                <button className="btn btn-danger" onClick={() => { this.props.delete(this.props.idBaiHoc) }}>Xóa bài học</button>
                <BaiHocChiTiet
                    idChuong={this.props.idChuong}
                    idBaiHoc={this.props.idBaiHoc}
                    key={this.props.idBaiHoc}
                    loadedContent={this.state.loadedContent}
                />
                <input type="text" ref="txtThemBaiTap" />
                <button className="btn btn-warning" onClick={this.add.bind(this)}>Thêm một bài tập</button>
                <CSSTransitionGroup
                transitionName="example"
                transitionEnterTimeout={800}
                transitionLeaveTimeout={800}>
                    {
                        this.state.idListBaiTap.map((obj,index)=>{
                            return <BaiTap key={obj.id} idBaiTap={obj.id} delete={this.delete}/>
                        })
                    }
                </CSSTransitionGroup>
            </div>
        )
    }

    componentDidMount() {
        let thisCom = this;
        $.post('/Admin/QuanLyBaiHoc', { code: 3, __RequestVerificationToken: _TOKEN, idBaiHoc: this.props.idBaiHoc }, function (data) {
            // console.log(data);
            thisCom.setState({ ten: data.ten, idListBaiTap: data.idListBaiTap });
        });
    }
}

class BaiTap extends React.Component {
    constructor() {
        super();
        this.state = {
            ten: 'Tên bài tập',
            idListCauHoi: [],
            
            loaded: false,
            showContent: false,
        }
        this.deleteQuestion = this.deleteQuestion.bind(this);
    }

    show() {
        if (!this.state.loaded) {
            let thisCom = this;
            $.post('/Admin/QuanLyBaiTap', { code: 1, __RequestVerificationToken: _TOKEN, idBaiTap: this.props.idBaiTap }, function (data) {
                console.log(data);
                thisCom.setState({ ten: data.ten, idListCauHoi: data.idListCauHoi });
            });
        }
        this.setState({showContent: true, loaded: true});
    }

    hide() {
        this.setState({showContent: false});
    }

    addQuestion() {
        let thisCom = this;
        $.post('/Admin/QuanLyBaiTap', { code: 6, __RequestVerificationToken: _TOKEN, idBaiTap: this.props.idBaiTap }, function (data) {
            // console.log(data);
            // console.log(thisCom.state.idListCauHoi);
            thisCom.state.idListCauHoi.unshift({id: parseInt(data)});
            thisCom.setState({idListCauHoi: thisCom.state.idListCauHoi});
        });
    }

    deleteQuestion(idCauHoi) {
        let thisCom = this;
        let index = -1;
        $.post('/Admin/QuanLyBaiTap', { code: 9, __RequestVerificationToken: _TOKEN, idCauHoi: idCauHoi }, function (data) {
            console.log(data);
            for (let i = 0; i < thisCom.state.idListCauHoi.length; i++) {
            if (thisCom.state.idListCauHoi[i].id == idCauHoi) {
                    index = i;
                    break;
                }
            }
            let arrayList = thisCom.state.idListCauHoi;
            arrayList.splice(index, 1);
            thisCom.setState({idListCauHoi: arrayList});
        });
    }

    render() {
        return (
            <div className="baiTap-wrapper">
                {
                    this.state.showContent
                    ?
                    <button className="btn btn-primary" onClick={this.hide.bind(this)}>Ẩn chi tiết bài tập {this.state.ten}</button>
                    :
                    <button className="btn btn-primary" onClick={this.show.bind(this)}>Hiện chi tiết bài tập {this.state.ten}</button>
                }
                {
                    this.state.showContent
                    ?
                    <div>
                        <button className="btn btn-warning" onClick={this.addQuestion.bind(this)}>Thêm một câu hỏi</button>
                        <br />
                        {this.state.ten}
                        <br />
                        <CSSTransitionGroup
                            transitionName="example"
                            transitionEnterTimeout={800}
                            transitionLeaveTimeout={800}>
                        {
                            this.state.idListCauHoi.map((obj, index) => {
                                return <CauHoi 
                                idCauHoi={obj.id} 
                                key={obj.id} 
                                deleteQuestion={this.deleteQuestion} />
                            })
                        }
                        </CSSTransitionGroup>
                    </div>
                    :
                    ""
                }
                <button className="btn btn-danger" onClick={() => {this.props.delete(this.props.idBaiTap)}}>Xóa bài tập</button>
            </div>
        )
    }
    componentDidMount() {
        
    }
}

class CauHoi extends React.Component {
    constructor() {
        super();
        this.state = {
            noiDung: 'Nội dung câu hỏi',
            cauTraLoi1: 'Câu trả lời 1',
            cauTraLoi2: 'Câu trả lời 2',
            cauTraLoi3: 'Câu trả lời 3',
            cauTraLoi4: 'Câu trả lời 4',
            cauDung: 1,

            editMode: false,

            editAnswer1Mode: false,
            editAnswer2Mode: false,
            editAnswer3Mode: false,
            editAnswer4Mode: false,
        }
    }

    changeRightAnswer(code) {
        
        let thisCom = this;
        $.post('/Admin/QuanLyBaiTap', { code: 3, __RequestVerificationToken: _TOKEN, idCauHoi: this.props.idCauHoi, cauDung: code }, function (data) {
            console.log(data);
            thisCom.setState({cauDung: code});
        });
        
    }

    editEnter() {
        this.setState({editMode: true});
    }

    editCancel() {
        this.setState({editMode: false});
    }

    editConfirm() {
        // Ajax

        let editedValue = CKEDITOR.instances['editorCauHoi' + this.props.idCauHoi].getData();
        let decodedHtml = editedValue;
        editedValue = editedValue.escapeHTML();
        let thisCom = this;
        
        $.post('/Admin/QuanLyBaiTap', {
            code: 8, __RequestVerificationToken: _TOKEN,
            idCauHoi: this.props.idCauHoi,
            noiDungHtml: editedValue
        }
        , function (data) {
            console.log(data);
            thisCom.setState({noiDung: decodedHtml, editMode: false})
        });
    }

    editAnswer(code) {
        switch (code) {
            case 1:
                this.setState({editAnswer1Mode: true});
            break;
            case 2:
                this.setState({editAnswer2Mode: true});
            break;
            case 3:
                this.setState({editAnswer3Mode: true});
            break;
            case 4:
                this.setState({editAnswer4Mode: true});
            break;
        }
    }

    save(viTriCauHoi) {
        let noiDungCauTraLoi = "";
        
        switch (viTriCauHoi) {
            case 1:
                noiDungCauTraLoi = this.refs.txtChange1.value;
                this.setState({editAnswer1Mode: false});
            break;
            case 2:
                noiDungCauTraLoi = this.refs.txtChange2.value;
                this.setState({editAnswer2Mode: false});
            break;
            case 3:
                noiDungCauTraLoi = this.refs.txtChange3.value;
                this.setState({editAnswer3Mode: false});
            break;
            case 4:
                noiDungCauTraLoi = this.refs.txtChange4.value;
                this.setState({editAnswer4Mode: false});
            break;
        }

        let thisCom = this;
        $.post('/Admin/QuanLyBaiTap', { code: 7, __RequestVerificationToken: _TOKEN, idCauHoi: this.props.idCauHoi, noiDungCauTraLoi: noiDungCauTraLoi, cauDung: viTriCauHoi }, function (data) {
            console.log(data);
            switch (viTriCauHoi) {
                case 1:
                    thisCom.setState({editAnswer1Mode: false, cauTraLoi1: noiDungCauTraLoi});
                break;
                case 2:
                    thisCom.setState({editAnswer2Mode: false, cauTraLoi2: noiDungCauTraLoi});
                break;
                case 3:
                    thisCom.setState({editAnswer3Mode: false, cauTraLoi3: noiDungCauTraLoi});
                break;
                case 4:
                    thisCom.setState({editAnswer4Mode: false, cauTraLoi4: noiDungCauTraLoi});
                break;
            }
        });
        
    }

    render() {
        if (this.state.editMode) {
            return (
                <div>
                    <textarea 
                        defaultValue={this.state.noiDung} 
                        name={'editorCauHoi' + this.props.idCauHoi} 
                        id={'editorCauHoi' + this.props.idCauHoi}>
                    </textarea>
                    <button className="btn btn-primary" onClick={this.editCancel.bind(this)}>Hủy sửa nội câu hỏi</button>
                    <button className="btn btn-primary" onClick={this.editConfirm.bind(this)}>Chấp nhận sửa nội dung câu hỏi</button>
                </div>
            )
        }

        return(
            <div>
                <div dangerouslySetInnerHTML={{ __html: this.state.noiDung }}></div>
                <div>
                    <input onChange={() => this.changeRightAnswer(1)} checked={this.state.cauDung == 1 ? "checked" : ""}  type="radio" name={"cauhoi_"+this.props.idCauHoi} />
                    { !this.state.editAnswer1Mode 
                         ?
                         <span>
                            {this.state.cauTraLoi1}
                            <button className="btn btn-primary" onClick={() => {this.editAnswer(1)}}>Sửa</button>
                         </span>
                         :
                         <span>
                            <input ref="txtChange1" type="text" defaultValue={this.state.cauTraLoi1}/>
                            <button className="btn btn-primary" onClick={() => {this.save(1)}}>Lưu</button>
                         </span>
                    }
                    <br />
                    <input onChange={() => this.changeRightAnswer(2)}  checked={this.state.cauDung == 2 ? "checked" : ""} type="radio" name={"cauhoi_"+this.props.idCauHoi}  />
                    { !this.state.editAnswer2Mode 
                         ?
                         <span>
                            {this.state.cauTraLoi2}
                            <button className="btn btn-primary" onClick={() => {this.editAnswer(2)}}>Sửa</button>
                         </span>
                         :
                         <span>
                            <input ref="txtChange2" type="text" defaultValue={this.state.cauTraLoi2}/>
                            <button className="btn btn-primary" onClick={() => {this.save(2)}}>Lưu</button>
                         </span>
                    }
                    <br />
                    <input onChange={() => this.changeRightAnswer(3)} checked={this.state.cauDung == 3 ? "checked" : ""} type="radio" name={"cauhoi_"+this.props.idCauHoi}  />
                    { !this.state.editAnswer3Mode 
                         ?
                         <span>
                            {this.state.cauTraLoi3}
                            <button className="btn btn-primary" onClick={() => {this.editAnswer(3)}}>Sửa</button>
                         </span>
                         :
                         <span>
                            <input ref="txtChange3" type="text" defaultValue={this.state.cauTraLoi3}/>
                            <button className="btn btn-primary" onClick={() => {this.save(3)}}>Lưu</button>
                         </span>
                    }
                    <br />
                    <input onChange={() => this.changeRightAnswer(4)} checked={this.state.cauDung == 4 ? "checked" : ""} type="radio" name={"cauhoi_"+this.props.idCauHoi}  />
                    { !this.state.editAnswer4Mode 
                         ?
                         <span>
                            {this.state.cauTraLoi4}
                            <button className="btn btn-primary" onClick={() => {this.editAnswer(4)}}>Sửa</button>
                         </span>
                         :
                         <span>
                            <input ref="txtChange4" type="text" defaultValue={this.state.cauTraLoi4}/>
                            <button className="btn btn-primary" onClick={() => {this.save(4)}}>Lưu</button>
                         </span>
                    }
                    <br />
                    <button className="btn btn-primary" onClick={this.editEnter.bind(this)}>Sửa nội dung câu hỏi</button>
                </div>
                <button className="btn btn-danger" onClick={() => {this.props.deleteQuestion(this.props.idCauHoi)}}>Xóa câu hỏi</button>
            </div>
        )
    }

    componentDidUpdate() {
        if (this.state.editMode)
            CKEDITOR.replace('editorCauHoi' + this.props.idCauHoi, { height: '600px' });
        else {
            if (CKEDITOR.instances['editorCauHoi' + this.props.idCauHoi])
                CKEDITOR.instances['editorCauHoi' + this.props.idCauHoi].destroy(true);
        }
    }

    componentDidMount() {
        let thisCom = this;
        $.post('/Admin/QuanLyBaiTap', { code: 2, __RequestVerificationToken: _TOKEN, idCauHoi: this.props.idCauHoi }, function (data) {
            console.log(data);
            let decodedHtml = new DOMParser().parseFromString(data.noiDung, "text/html").documentElement.textContent;
            thisCom.setState({
                noiDung: decodedHtml,
                cauTraLoi1: data.cauTraLoi1,
                cauTraLoi2: data.cauTraLoi2,
                cauTraLoi3: data.cauTraLoi3,
                cauTraLoi4: data.cauTraLoi4,
                cauDung: parseInt(data.cauDung),
            })
        });
    }
}

class BaiHocChiTiet extends React.Component {
    constructor() {
        super();
        this.state = {
            // Attribute states
            noidung: 'Nội dung trống',
            // Control states
            editMode: false,
            loadedContent: false,
            showContent: false,
        }
    }

    show() {
        if (!this.props.loadedContent) {
            // console.log("loaded");
            let thisCom = this;
            $.post('/Admin/QuanLyBaiHoc', { code: 4, __RequestVerificationToken: _TOKEN, idBaiHoc: this.props.idBaiHoc }, function (data) {
                // console.log(data);
                let decodedHtml = new DOMParser().parseFromString(data.noidung, "text/html").documentElement.textContent;
                // console.log(decodedHtml);
                thisCom.setState({ noidung: decodedHtml });
            });
        }
        this.setState({ loadedContent: true, showContent: true });
    }

    hide() {
        this.setState({ showContent: false, editMode: false });
    }

    editEnter() {
        this.setState({ editMode: true })
    }

    editCancel() {
        this.setState({ editMode: false })
    }

    editConfirm() {
        let editedValue = CKEDITOR.instances['editor' + this.props.idBaiHoc].getData();
        let decodedHtml = editedValue;
        editedValue = editedValue.escapeHTML();
        // console.log(editedValue);
        let thisCom = this;
        let idChuong = this.props.idChuong;
        let idBaiHoc = this.props.idBaiHoc;
        this.setState({ noidung: decodedHtml, editMode: false });
        
        $.post('/Admin/QuanLyBaiHoc', {
            code: 9, __RequestVerificationToken: _TOKEN,
            idChuong: this.props.idBaiHoc,
            idBaiHoc: idBaiHoc,
            noiDungHtml: editedValue
        }
        , function (data) {
            // console.log(data);
        });
    }

    render() {
        return (
            <div className="chiTietBaiHoc-wrapper">
                {
                    !this.state.showContent
                        ?
                        <button className="btn btn-primary" onClick={() => this.show()}>Hiện chi tiết bài học</button>
                        :
                        <button className="btn btn-primary" onClick={() => this.hide()}>Ẩn chi tiết bài học</button>
                }
                {
                    this.state.showContent ?
                        (
                            !this.state.editMode
                                ?
                                <div className="noiDungBaiHoc" dangerouslySetInnerHTML={{ __html: this.state.noidung }}>
                                </div>
                                :
                                <textarea defaultValue={this.state.noidung} name={'editor' + this.props.idBaiHoc} id={'editor' + this.props.idBaiHoc}>
                                </textarea>
                        ) : ""
                }
                {
                    this.state.showContent ?
                        (
                            !this.state.editMode ?
                                <button className="btn btn-primary" onClick={this.editEnter.bind(this)}>Sửa nội dung bài học</button>
                                :
                                <div>
                                    <button className="btn btn-primary" onClick={this.editConfirm.bind(this)}>Chấp nhận sửa</button>
                                    <button className="btn btn-primary" onClick={this.editCancel.bind(this)}>Hủy bỏ</button>
                                </div>
                        ) : ""
                }
            </div>
        )
    }

    componentDidUpdate() {
        if (this.state.editMode)
            CKEDITOR.replace('editor' + this.props.idBaiHoc, { height: '600px' });
        else {
            if (CKEDITOR.instances['editor' + this.props.idBaiHoc])
                CKEDITOR.instances['editor' + this.props.idBaiHoc].destroy(true);
        }
    }
    componentDidMount() {
        
    }

}

ReactDOM.render(<QuanLyChuong />, document.getElementById('app'));