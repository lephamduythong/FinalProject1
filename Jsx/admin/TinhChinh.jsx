import React from 'react';
import ReactDOM from 'react-dom';
import ReactCSSTransitionGroup from 'react-addons-css-transition-group';
import $ from 'jquery';

export default class TinhChinh extends React.Component {
    constructor() {
        super();
    }

    toogleCheck(code) {
        // console.log($('#chk_' + code).prop('checked','checked').prop('checked'));
        let checkBox = $('#chk_' + code);
        console.log(checkBox.prop('checked'));
        if (checkBox.prop('checked')) {
            checkBox.prop('checked','checked');
        }
        else {
            checkBox.removeProp('checked');
        }
    }

    render() {
        return (
            <div>
                <p className="config-title">Bật quảng cáo:</p>
                <label className="switch">
                    <input onClick={() => this.toogleCheck(1)} id="chk_1"  type="checkbox" defaultChecked/>
                    <div className="slider"></div>
                </label>
            </div>
        )
    }
}

ReactDOM.render(<TinhChinh />, document.getElementById('app'));