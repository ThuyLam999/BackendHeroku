pipeline {
    agent any 
    stages {
        stage('Push to Heroku registry') {
            steps {
               withEnv(['PATH+HEROKU=C:\\Program Files\\heroku\\bin']) {
                    withCredentials([usernamePassword(credentialsId: 'herokuid', passwordVariable: 'password', usernameVariable: 'username')]) {
                        bat 'heroku login'
                        bat 'heroku container:release web --app=test-api-9'
                    }
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
