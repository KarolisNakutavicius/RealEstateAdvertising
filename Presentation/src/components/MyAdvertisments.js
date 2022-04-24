import React, { Component } from 'react'
import AdvertisementService from '../Services/AdvertisementService'
import Advertisment from './Advertisment';

export default class MyAdvertisments extends Component {

  constructor(props)
  {
    super(props);

    this.state = {
      advertisements:[]
    }
  }

  async componentDidMount()
  {
    var ads = await AdvertisementService.getMyAdvertisments();

    if(ads)
    {
      this.setState({
        advertisements:ads
      })
    }
  }

  render() {
    return (
      this.state.advertisements.map(ad => {
          return <Advertisment ad={ad}/>
        })
    )
  }
}
