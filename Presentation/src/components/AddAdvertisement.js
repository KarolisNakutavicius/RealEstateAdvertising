import React, { Component } from 'react'
import Form from "react-validation/build/form";
import Input from "react-validation/build/input";
import CheckButton from "react-validation/build/button";
import ValidationHelper from '../Helpers/ValidationHelper'

export default class AddAdvertisement extends Component {

  constructor(props) {
    super(props);

    this.state = {
      successful: false,
      message: ""
    };
  }

  handleCreate()
  {
    
  }

  render() {
    return (
      <div className="col-md-12">
        <div className="card card-container">
          <Form
            onSubmit={this.handleCreate}
            ref={c => {
              this.form = c;
            }}>
            <div>
              <div className="form-group">
                <label htmlFor="email">Name</label>
                <Input
                  type="text"
                  className="form-control"
                  name="text"
                  validations={[ValidationHelper.required]}
                />
              </div>
              <div className="form-group">
                <label htmlFor="Type">Purpose</label>
                <select className='form-select'>
                  <option value="0">Sell</option>
                  <option value="1">Rent</option>
                </select>
              </div>
              <div className="form-group">
                <label htmlFor="Type">Type</label>
                <select className='form-select'>
                  <option value="1">Residential</option>
                  <option value="2">Commercial</option>
                  <option selected value="3">Industrial</option>
                  <option value="4">Land</option>
                  <option value="5">SpecialPurpose</option>
                </select>
              </div>
              <div className="form-group">
                <label htmlFor="email">Size (in m²)</label>
                <Input
                  type="number" min="1"
                  className="form-control"
                  name="text"
                  validations={[ValidationHelper.required]}
                />
              </div>
              <div className="form-group">
                <label htmlFor="email">City</label>
                <Input
                  type="text"
                  className="form-control"
                  name="text"
                  validations={[ValidationHelper.required]}
                />
              </div>
              <div className="form-group">
                <label htmlFor="email">Street</label>
                <Input
                  type="text"
                  className="form-control"
                  name="text"
                  validations={[ValidationHelper.required]}
                />
              </div>
              <div className="form-group">
                <label htmlFor="email">Description</label>
                <textarea
                  type="text"
                  className="form-control"
                  name="text"
                  validations={[ValidationHelper.required]}
                />
              </div>
              <div className="form-group mt-3">
                <CheckButton className="btn btn-primary btn-block"
                  ref={c => {
                    this.checkBtn = c;
                  }}
                >Create
                </CheckButton>
              </div>
            </div>
            {this.state.message && (
              <div className="form-group mt-3">
                <div
                  className={
                    this.state.successful
                      ? "alert alert-success"
                      : "alert alert-danger"
                  }
                  role="alert"
                >
                  {this.state.message}
                </div>
              </div>
            )}
          </Form>
        </div>
      </div>
    )
  }
}
