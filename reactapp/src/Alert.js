import React, {Component} from 'react'

class Alert extends Component
{
    renderResult = this.props.renderResult
    redirectUrl = this.props.redirectUrl

    render(){
        const redirectUrl = this.props.redirectUrl

        if(redirectUrl === '')
            return null;

        return(
            <div className="alert alert-warning alert-dismissible fade show col-md-6 mx-auto" role="alert">
                <strong>Short link: <a id="redirectUrl" href={redirectUrl}>{redirectUrl}</a></strong>
                <button type="button" className="btn" aria-label="Copy" onClick= {() => this.copyUrl(redirectUrl)}>Copy</button>
                <button type="button" className="btn-close" aria-label="Close" onClick= {() => this.close()}></button>
            </div>
        )
    }

    async copyUrl(redirectUrl)
    {
        await navigator.clipboard.writeText(redirectUrl);
    }

    close()
    {
        this.renderResult('');
    }
}

export default Alert