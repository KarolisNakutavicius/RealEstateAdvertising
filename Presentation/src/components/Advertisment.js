import React, { Component } from 'react'

export default class Advertisment extends Component {
    render() {
        return (
            <div className="card" style={{width:"18rem"}}>
                <img className="card-img-top" src={`data:image/png;base64,${this.props.ad.image}`} alt="Image cannot be loaded" />
                <div className="card-body">
                    <h5 className="card-title">{this.props.ad.name}</h5>
                    <label>City : {this.props.ad.city}</label>
                    <label>Number : {this.props.ad.number}</label>
                    <label>Size : {this.props.ad.size} m²</label>
                    <label>Zip : {this.props.ad.zip}</label>

                    <label className='mt-5'>Description: </label>
                    <p className="card-text">{this.props.ad.description}</p>
                </div>
            </div>
        )
    }
}
