import React from 'react';
import ReactDOM from 'react-dom';
import ReactCSSTransitionGroup from 'react-addons-css-transition-group';
import $ from 'jquery';
import 'jquery-ui-dist/jquery-ui.js';

let selectedHocSinhId = -1;

class AutoCompleteListHocSinh extends React.Component {
    constructor() {
        super();
    }
    render() {
        return (
            <div className="ui-widget">
                <label htmlFor="tags">Tìm một học sinh: &nbsp;&nbsp;</label>
                <input style={{width: '100%'}} id="tags" />
            </div>
        )
    }
    componentDidMount() {
        // Load tất cả danh sách học sinh
        let thisCom = this;
        $.post("/Admin/QuanLyHocPhi", { code: 2 }, function (data) {
            console.log(data);
            for (let i = 0; i < data.length; i++) {
                data[i].label = data[i].hoTen + " - " + data[i].email;
            }
            thisCom.setState({ listHocSinh: data });

            $("#tags").autocomplete({
                source: function (request, response) {
                    let data2 = data;
                    response(data2);
                },
                select: function (event, ui) {
                    // console.log(ui.item.hocSinhId);
                    selectedHocSinhId = ui.item.hocSinhId;
                    thisCom.props.selectedHocSinh(ui.item.hocSinhId);
                }
            });
        });
    }
}



export default class QuanLyHocPhi extends React.Component {
    constructor() {
        super();
        this.state = {
            idListHocPhi: [],
        }
    }

    getListHocSinh() {
        return this.refs.autoCompleteListHocSinh.state.listHocSinh;
    }

    selectedHocSinh(hocSinhId) {
        let thisCom = this;
        $.post("/Admin/QuanLyHocPhi", { code: 4, hocSinhId: hocSinhId }, function (data) {
            // console.log(data);
            let array = [];
            for (let i = 0; i < data.length; i++) {
                array.push(data[i].hocPhiId);
            }
            thisCom.setState({idListHocPhi: array});
        })
    }

    showAddBox() {
        $('#addBox').fadeIn();
    }

    addNew() {
        let thisCom = this;
        $.post("/Admin/QuanLyHocPhi", { 
            code: 5, 
            ngayThanhToan: this.refs.dateNgayThanhToan.value,
            ngayHetHan: this.refs.dateNgayHetHan.value,
            soTien: this.refs.txtSoTien.value,
            hocSinhId: selectedHocSinhId,
        }, function (data) {
            thisCom.state.idListHocPhi.unshift(data);
            thisCom.setState(thisCom.state);
        });
    }

    deleteHocPhi(hocPhiId) {
        let thisCom = this;
        $.post("/Admin/QuanLyHocPhi", {
            code: 6,
            hocPhiId: hocPhiId,
        }, function(data) {
            // console.log(data)
            let index = -1;
            for (let i = 0; i < thisCom.state.idListHocPhi.length; i++) {
                if (thisCom.state.idListHocPhi[i] == hocPhiId) {
                    index = i;
                }
            }
            thisCom.state.idListHocPhi.splice(index, 1);
            thisCom.setState(thisCom.state);
        });
    }

    render() {
        return (
            <div>
                <AutoCompleteListHocSinh 
                    ref="autoCompleteListHocSinh" 
                    selectedHocSinh={this.selectedHocSinh.bind(this)} 
                />
                <br />
                <button onClick={() => this.showAddBox()} className="btn btn-primary">Thêm một khoản thanh toán</button>
                
                <br /> <br />
                
                <div id="addBox">
                    <table>
                        <tbody>
                        <tr>
                            <td>
                                Số tiền: 
                            </td>
                            <td>
                                <input type="number" ref="txtSoTien"/>
                            </td>
                        </tr>
                        <tr>
                            <td>Ngày thanh toán: </td>
                            <td><input type="date" ref="dateNgayThanhToan" /> </td>
                        </tr>
                        <tr>
                            <td>Ngày hết hạn: </td>
                            <td><input type="date" ref="dateNgayHetHan" /> </td>
                        </tr>
                        </tbody>
                    </table>
                    <button className="btn btn-warning" onClick={() => this.addNew()}>Thêm</button>
                </div>

                <br />

                <table className="table table-bordered">
                    <thead>
                        <tr>
                            <th>Id</th>
                            <th>Học Sinh</th>
                            <th>Email</th>
                            <th>Ngày thanh toán</th>
                            <th>Ngày hết hạn</th>
                            <th>Số tiền</th>
                            <th> </th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            this.state.idListHocPhi.map((object, index) => {
                                return <HocPhi 
                                            key={object} 
                                            hocPhiId={object} 
                                            listHocSinh={this.getListHocSinh.bind(this)}
                                            deleteHocPhi={this.deleteHocPhi.bind(this)}
                                        />
                            })
                        }
                    </tbody>
                </table>
            </div>
        )
    }
    componentDidMount() {
        
        $('#addBox').hide();
        
        // this.aFunction();
        let thisCom = this;
        // Load tất cả lịch sử giao dịch học phí
        $.post("/Admin/QuanLyHocPhi", { code: 1 }, function (data) {
            let array = [];
            for (let i = 0; i < data.length; i++) {
                array.push(data[i].hocPhiId);
            }
            console.log(data);
            
            thisCom.setState({idListHocPhi: array});
        });

    }
}

class HocPhi extends React.Component {
    constructor() {
        super();
        this.state = {
            hocPhiId: -1,
            ngayHetHan: "",
            ngayThanhToan: "",
            hoTen: "",
            email: "",
            soTien: 0,
        }
    }
    render() {
        return (
            <tr>
                <td>{this.state.hocPhiId}</td>
                <td>{this.state.hoTen}</td>
                <td>{this.state.email}</td>
                <td>{this.state.ngayThanhToan}</td>    
                <td>{this.state.ngayHetHan}</td>
                <td>{this.state.soTien}</td>      
                <td><button onClick={() => {this.props.deleteHocPhi(this.props.hocPhiId)}} className="btn btn-danger">Xóa</button></td>  
            </tr>
        )
    }
    componentDidMount() {
        let thisCom = this;
        // console.log(this.props.getListHocSinh());
        // Load tất cả lịch sử giao dịch học phí
        $.post("/Admin/QuanLyHocPhi", { code: 3, hocPhiId: this.props.hocPhiId }, function (data) {
            console.log(data);
            let listHocSinh = thisCom.props.listHocSinh();
            
            for (let i = 0; i < listHocSinh.length; i++) {
                if (listHocSinh[i].hocSinhId == data.hocSinhId) {
                    
                    let ngayHetHan = new Date(data.ngayHetHan);
                    let ngayThanhToan = new Date(data.ngayThanhToan);
                    
                    thisCom.setState({
                        hocPhiId: data.hocPhiId,
                        ngayHetHan: ngayHetHan.getDate() + '-' + ngayHetHan.getMonth() + '-' + ngayHetHan.getFullYear(),
                        ngayThanhToan: ngayThanhToan.getDate() + '-' + ngayThanhToan.getMonth() + '-' + ngayThanhToan.getFullYear(),
                        hoTen: listHocSinh[i].hoTen,
                        email: listHocSinh[i].email,
                        soTien: data.soTien,
                    })
                    break;
                }
            }

        });
    }
}

ReactDOM.render(<QuanLyHocPhi />, document.getElementById('app'));