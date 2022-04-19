import React, { Component } from 'react'
import Form from "react-validation/build/form";
import Input from "react-validation/build/input";
import CheckButton from "react-validation/build/button";
import ValidationHelper from '../Helpers/ValidationHelper'

export default class AddAdvertisement extends Component {

  constructor(props) {
    super(props);

    this.handleCreate = this.handleCreate.bind(this);
    this.onChangeName = this.onChangeName.bind(this);
    this.onPurposeChange = this.onPurposeChange.bind(this);
    this.onTypeChange = this.onTypeChange.bind(this);
    this.onSizeChange = this.onSizeChange.bind(this);
    this.onCityChange = this.onCityChange.bind(this);
    this.onStreetChange = this.onStreetChange.bind(this);
    this.onNumberChange = this.onNumberChange.bind(this);
    this.onDescriptionChange = this.onDescriptionChange.bind(this);
    this.onZipChange = this.onZipChange.bind(this);
    
    this.state = {
      name:"",
      purpose:0,
      type:0,
      size:1,
      city:"Vilnius",
      number:undefined,
      street:"",
      zip:"",
      description:"",
      successful: false,
      message: ""
    };
  }

  handleCreate(e)
  {   
     e.preventDefault();

    this.form.validateAll();
    if (this.checkBtn.context._errors.length !== 0){
      return;
    }

    var request = 
    {
      Name: this.state.name,
      IsRent: this.state.purpose,
      Type:this.state.type,
      Size:this.state.size,
      City:this.state.city,
      Street:this.state.street,
      Number: this.state.number,
      Zip: this.state.Zip,
      Description:this.state.description,
    }
  }

  onChangeName(e) {
    this.setState(
      {
        name: e.target.value
      });
  }

  onPurposeChange(e)
  {
    this.setState(
      {
        purpose: e.target.value
      }); 
  }

  onTypeChange(e){
    this.setState(
      {
        type: e.target.value
      }); 
  }

  onSizeChange(e){
    this.setState(
      {
        size: e.target.value
      }); 
  }

  onCityChange(e){
    this.setState(
      {
        city: e.target.value
      }); 
  }

  onStreetChange(e)
  {
    this.setState(
      {
        street: e.target.value
      }); 
  }

  onNumberChange(e){
    this.setState(
      {
        number: e.target.value
      }); 
  }

  onDescriptionChange(e){
    this.setState(
      {
        description: e.target.value
      }); 
  }

  onZipChange(e){
    this.setState(
      {
        zip: e.target.value
      }); 
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
                <label>Name</label>
                <Input
                  type="text"
                  className="form-control"
                  name="text"
                  value={this.state.name}
                  onChange={this.onChangeName}
                  validations={[ValidationHelper.required]}
                />
              </div>
              <div className="form-group">
                <label htmlFor="Type">Purpose</label>
                <select className='form-select' onChange={this.onPurposeChange} value={this.state.purpose}>
                  <option value="0">Sell</option>
                  <option value="1">Rent</option>
                </select>
              </div>
              <div className="form-group">
                <label htmlFor="Type">Type</label>
                <select className='form-select' onChange={this.onTypeChange} value={this.state.type}>
                  <option value="1">Residential</option>
                  <option value="2">Commercial</option>
                  <option selected value="3">Industrial</option>
                  <option value="4">Land</option>
                  <option value="5">SpecialPurpose</option>
                </select>
              </div>
              <div className="form-group">
                <label>Size (in m²)</label>
                <Input onChange={this.onSizeChange} value={this.state.size}
                  type="number" min="1"
                  className="form-control"
                  name="text"
                  validations={[ValidationHelper.required]}
                />
              </div>
              <div className="form-group">
                <label>City</label>
                <Input
                  type="text"
                  className="form-control"
                  name="text"
                  value={this.state.city}
                  onChange={this.onCityChange}
                  validations={[ValidationHelper.required]}
                />
              </div>
              <div className="form-group">
                <label>Street</label>
                <Input
                  value={this.state.stret}
                  onChange={this.onStreetChange}
                  type="text"
                  className="form-control"
                  name="text"
                  validations={[ValidationHelper.required]}
                />
              </div>
              <div className="form-group">
                <label htmlFor="email">Number</label>
                <Input
                  value={this.state.number}
                  onChange={this.onNumberChange}
                  type="number"
                  className="form-control"
                  name="text"
                />
              </div>
              <div className="form-group">
                <label htmlFor="email">Zip</label>
                <Input
                  value={this.state.zip}
                  onChange={this.onZipChange}
                  type="text"
                  className="form-control"
                  name="text"
                />
              </div>
              <div className="form-group">
                <label htmlFor="email">Description</label>
                <textarea
                  value={this.state.description}
                  onChange={this.onDescriptionChange}
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
