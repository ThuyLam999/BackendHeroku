pipeline {
    agent any 
    stages {
        stage('Push to Heroku registry') {
            steps {
               withCredentials([usernamePassword(credentialsId: 'herokuid', passwordVariable: 'password', usernameVariable: 'username')]) {
                    bat 'C:\\Program Files\\heroku\\binheroku container:release web --app=test-api-9'
                }
            }          
        }

        stage('Cleanup') {
            steps {
                deleteDir()
            }
        }
    }
}
