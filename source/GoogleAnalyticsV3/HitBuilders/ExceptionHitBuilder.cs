/*
  Copyright 2014 Google Inc. All rights reserved.

  Licensed under the Apache License, Version 2.0 (the "License");
  you may not use this file except in compliance with the License.
  You may obtain a copy of the License at

      http://www.apache.org/licenses/LICENSE-2.0

  Unless required by applicable law or agreed to in writing, software
  distributed under the License is distributed on an "AS IS" BASIS,
  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
  See the License for the specific language governing permissions and
  limitations under the License.
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ExceptionHitBuilder : HitBuilder<ExceptionHitBuilder> {

  private string exceptionDescription  = "";
  private bool fatal = false;

  public string GetExceptionDescription(){
    return exceptionDescription;
  }

  public ExceptionHitBuilder SetExceptionDescription(string exceptionDescription) {
    if(exceptionDescription != null){
      this.exceptionDescription = exceptionDescription;
    }
    return this;
  }

  public bool IsFatal(){
    return fatal;
  }

  public ExceptionHitBuilder SetFatal(bool fatal) {
    this.fatal = fatal;
    return this;
  }

  public override ExceptionHitBuilder GetThis(){
    return this;
  }

  public override ExceptionHitBuilder Validate(){
    return this;
  }
}
