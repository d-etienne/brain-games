# from config import sender, to, service
from config import sender, to
from email.mime.text import MIMEText
# from email.mime.multipart import MIMEMultipart
import base64


def create_message(sender, to, subject, message_text):
    """
    Create a message for an email.

    Args:
        sender: Email address of the sender.
        to: Email address of the receiver.
        subject: The subject of the email message.
        message_text: The text of the email message.

    Returns:
        An object containing a base64url encoded email object.
    """
    message = MIMEText(message_text)
    message['to'] = to
    message['from'] = sender
    message['subject'] = subject
    return {'raw': base64.urlsafe_b64encode(message.as_string().encode()).decode()}


def send_message(service, user_id, message):
    """
    Send an email message.

    Args:
        service: Authorized Gmail API service instance.
        user_id: User's email address. The special value "me"
        can be used to indicate the authenticated user.
        message: Message to be sent.

    Returns:
        Sent Message.
    """
    try:
        message = (service.users().messages().send(userId=user_id, body=message).execute())
        print('Message Id: %s' % message['id'])
        return message
    except(errors.HttpError, error):
        print('An error occurred: %s' % error)


name = input("Hello and welcome to the team! What is your full name?: ")
unityID = input("What is your Unity ID?: ")
user = "Name: " + name + ", Unity ID: " + unityID

messageToSend = create_message(sender, to, 'New Brain Games Member', user)

# send_message(service, "me", messageToSend)
